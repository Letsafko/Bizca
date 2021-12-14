namespace Bizca.Bff.Infrastructure.Persistance
{
    using Bizca.Bff.Domain.Entities.Enumerations.Subscription;
    using Bizca.Bff.Domain.Entities.Subscription;
    using Bizca.Bff.Domain.Entities.User;
    using Bizca.Bff.Domain.Entities.User.ValueObjects;
    using Bizca.Bff.Domain.Enumerations;
    using Bizca.Bff.Domain.Referentials.Bundle;
    using Bizca.Bff.Domain.Referentials.Bundle.ValueObjects;
    using Bizca.Bff.Domain.Referentials.Procedure;
    using Bizca.Bff.Domain.Referentials.Procedure.ValueObjects;
    using Bizca.Core.Domain;
    using Dapper;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;

    public sealed class UserRepository : IUserRepository
    {
        private readonly IUnitOfWork unitOfWork;
        public UserRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        private const string createUserStoredProcedure = "[bff].[usp_create_user]";
        private const string updateUserStoredProcedure = "[bff].[usp_update_user]";
        private const string getUserStoredProcedure = "[bff].[usp_get_user]";
        public async Task<User> GetByExternalUserIdAsync(string externalUserId)
        {
            var parameters = new
            {
                externalUserId
            };
            return await BuildUserAsync(parameters);
        }
        public async Task<User> GetByPhoneNumberAsync(string phoneNumber)
        {
            var parameters = new
            {
                phone = phoneNumber
            };
            return await BuildUserAsync(parameters);
        }
        public async Task<User> GetByEmailAsync(string email)
        {
            var parameters = new
            {
                email
            };
            return await BuildUserAsync(parameters);
        }
        public async Task<bool> UpdateAsync(User user)
        {
            var parameters = new
            {
                confirmationStatus = (short)user.UserProfile.ChannelConfirmationStatus,
                activationStatus = (short)user.UserProfile.ChannelActivationStatus,
                externalUserId = user.UserIdentifier.ExternalUserId,
                civilityId = (byte)user.UserProfile.Civility,
                phoneNumber = user.UserProfile.PhoneNumber,
                firstName = user.UserProfile.FirstName,
                lastName = user.UserProfile.LastName,
                whatsapp = user.UserProfile.Whatsapp,
                email = user.UserProfile.Email,
                roleId = (int)user.Role,
                rowversion = user.GetRowVersion()
            };

            return await unitOfWork.Connection
                .ExecuteScalarAsync<int>(updateUserStoredProcedure,
                    parameters,
                    unitOfWork.Transaction,
                    commandType: CommandType.StoredProcedure)
                .ConfigureAwait(false) > 0;
        }
        public async Task<bool> AddAsync(User user)
        {
            var parameters = new
            {
                confirmationStatus = (short)user.UserProfile.ChannelConfirmationStatus,
                activationStatus = (short)user.UserProfile.ChannelActivationStatus,
                externalUserId = user.UserIdentifier.ExternalUserId,
                civilityId = (byte)user.UserProfile.Civility,
                phoneNumber = user.UserProfile.PhoneNumber,
                firstName = user.UserProfile.FirstName,
                lastName = user.UserProfile.LastName,
                whatsapp = user.UserProfile.Whatsapp,
                email = user.UserProfile.Email,
                roleId = (int)user.Role
            };

            return await unitOfWork.Connection
                .ExecuteScalarAsync<int>(createUserStoredProcedure,
                    parameters,
                    unitOfWork.Transaction,
                    commandType: CommandType.StoredProcedure)
                .ConfigureAwait(false) > 0;
        }

        #region private helpers

        private async Task<User> BuildUserAsync(object parameters)
        {
            SqlMapper.GridReader gridReader = await unitOfWork.Connection
                    .QueryMultipleAsync(getUserStoredProcedure,
                            parameters,
                            unitOfWork.Transaction,
                            commandType: CommandType.StoredProcedure)
                    .ConfigureAwait(false);

            (dynamic user, IEnumerable<dynamic> dynamycSsubscriptions) = GetEntities(gridReader);
            if (user is null)
            {
                return default;
            }

            UserProfile userProfile = GetUserProfile(user);
            var userIdentifier = new UserIdentifier((int)user.userId, user.externalUserId);
            IEnumerable<Subscription> subscriptions = BuildSubscriptions(dynamycSsubscriptions);
            var userBuild = new User((int)user.userId,
                userIdentifier,
                userProfile,
                (Role)user.roleId,
                subscriptions?.ToList(),
                (byte[])user.rowversion);
            return userBuild;
        }
        private (dynamic user, IEnumerable<dynamic> subscriptions) GetEntities(SqlMapper.GridReader gridReader)
        {
            var result = new Dictionary<ResultName, IEnumerable<dynamic>>();
            while (!gridReader.IsConsumed)
            {
                string resultSetName = gridReader.Read<string>().FirstOrDefault();
                if (!string.IsNullOrEmpty(resultSetName))
                {
                    IEnumerable<dynamic> reader = gridReader.Read();
                    if (Enum.TryParse(resultSetName, true, out ResultName resultName))
                    {
                        result[resultName] = reader;
                    }
                }
            }
            return
            (
                result[ResultName.User].FirstOrDefault(),
                result[ResultName.Subscriptions]
            );
        }
        private IEnumerable<Subscription> BuildSubscriptions(IEnumerable<dynamic> subscriptions)
        {
            if (subscriptions?.Any() == true)
            {
                foreach (dynamic subscription in subscriptions)
                {
                    SubscriptionSettings subscriptionSettings = GetSubscriptionSettings(subscription);
                    UserSubscription UserSubscription = GetUserSubscription(subscription);
                    Procedure procedure = GetProcedure(subscription);
                    Bundle bundle = GetBundle(subscription);
                    Money money = GetMoney(subscription);
                    yield return new Subscription((int)subscription.subscriptionId,
                        subscription.subscriptionCode,
                        UserSubscription,
                        procedure,
                        bundle,
                        money,
                        subscriptionSettings,
                        (SubscriptionStatus)subscription.subscriptionStatusId);
                }
            }
        }
        private SubscriptionSettings GetSubscriptionSettings(dynamic subscription)
        {
            return subscription.bundleId is null
                ? default
                : new SubscriptionSettings((int)subscription.whatsappCounter,
                    (int)subscription.emailCounter,
                    (int)subscription.smsCounter,
                    (int)subscription.totalWhatsapp,
                    (int)subscription.totalEmail,
                    (int)subscription.totalSms,
                    subscription.beginDate,
                    subscription.endDate,
                    subscription.isFreeze);
        }
        private UserSubscription GetUserSubscription(dynamic subscription)
        {
            return new UserSubscription(subscription.firstName,
                subscription.lastName,
                subscription.phoneNumber,
                subscription.whatsapp,
                subscription.email);
        }
        private Procedure GetProcedure(dynamic subscription)
        {
            var procedureType = new ProcedureType((int)subscription.procedureTypeId, subscription.procedureTypeLabel);
            var organism = new Organism((int)subscription.organismId,
                    subscription.codeInsee,
                    subscription.organismName,
                    subscription.organismHref);

            return new Procedure(procedureType, organism, subscription.procedureHref);
        }
        private UserProfile GetUserProfile(dynamic user)
        {
            return new UserProfile((Civility)user.civilityId,
                user.firstName,
                user.lastName,
                user.phoneNumber,
                user.whatsapp,
                user.email,
                (ChannelConfirmationStatus)user.channelConfirmationStatus,
                (ChannelActivationStatus)user.channelActivationStatus);
        }
        private Bundle GetBundle(dynamic subscription)
        {
            if (subscription.bundleId is null)
                return default;

            var bundleIdentifier = new BundleIdentifier((int)subscription.bundleId,
                subscription.bundleCode,
                subscription.bundleLabel);

            var bundleSettings = new BundleSettings((int)subscription.intervalInWeeks,
                (int)subscription.bundleTotalWhatsapp,
                (int)subscription.bundleTotalEmail,
                (int)subscription.bundleTotalSms);

            var priority = Priority.GetByCode((int)subscription.priority);
            var money = new Money((decimal)subscription.price);
            return new Bundle(bundleIdentifier,
                bundleSettings,
                priority,
                money);
        }
        private Money GetMoney(dynamic subscription)
        {
            return subscription.bundleId is null
                ? default
                : new Money((decimal)subscription.amount);
        }

        #endregion

    }
}
