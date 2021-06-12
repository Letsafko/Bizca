namespace Bizca.Bff.Domain.Entities.User.Factories
{
    using Bizca.Bff.Domain.Entities.Subscription;
    using Bizca.Bff.Domain.Entities.User.Exceptions;
    using Bizca.Bff.Domain.Entities.User.ValueObjects;
    using Bizca.Bff.Domain.Enumerations;
    using Bizca.Bff.Domain.Referentials.Bundle;
    using Bizca.Bff.Domain.Referentials.Bundle.ValueObjects;
    using Bizca.Bff.Domain.Referentials.Procedure;
    using Bizca.Bff.Domain.Referentials.Procedure.ValueObjects;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public sealed class UserFactory : IUserFactory
    {
        private readonly ISubscriptionRepository subscriptionRepository;
        private readonly IUserRepository userRepository;
        public UserFactory(IUserRepository userRepository,
            ISubscriptionRepository subscriptionRepository)
        {
            this.subscriptionRepository = subscriptionRepository;
            this.userRepository = userRepository;

        }

        private const string partnerCode = "Bizca";
        public async Task<User> BuildAsync(string externalUserId)
        {
            (dynamic user, IEnumerable<dynamic> subscriptions) = await GetEntitiesAsync(externalUserId);
            if(user is null)
            {
                throw new UserDoesNotExistException($"user {externalUserId} does not exist.");
            }

            var subscriptionsAlreadyCreated = new List<Subscription>();
            if(subscriptions?.Any() == true)
            {
                foreach(dynamic subscription in subscriptions)
                {
                    Subscription subscriptionToAdd = GetSubscription(subscription);
                    subscriptionsAlreadyCreated.Add(subscriptionToAdd);
                }
            }

            UserProfile userProfile = GetUserProfile(user);
            var userBuild = new User((int)user.userId,
                new UserIdentifier(externalUserId, partnerCode),
                userProfile,
                subscriptionsAlreadyCreated);

            userBuild.SetRowVersion((byte[])user.rowversion);
            return userBuild;
        }
        public User Create(UserRequest request)
        {
            UserIdentifier userIdentifier = GetUserIdentifier(request);
            UserProfile userProfile = GetUserProfile(request);
            var user = new User(0, userIdentifier, userProfile);
            user.RegisterNewChannelCodeConfirmation(request.ExternalUserId, ChannelType.Email);
            return user;
        }

        #region private helpers

        private async Task<(dynamic user, IEnumerable<dynamic> subscriptions)> GetEntitiesAsync(string externalUserId)
        {
            Dictionary<ResultName, IEnumerable<dynamic>> entities = await userRepository.GetAsync(externalUserId);
            return
            (
                entities[ResultName.User].FirstOrDefault(),
                entities[ResultName.Subscriptions]
            );
        }
        private UserSubscription GetUserSubscription(dynamic subscription)
        {
            return new UserSubscription(subscription.firstName,
                subscription.lastName,
                subscription.phoneNumber,
                subscription.whatsapp,
                subscription.email);
        }
        private UserIdentifier GetUserIdentifier(UserRequest request)
        {
            return new UserIdentifier(request.ExternalUserId, request.PartnerCode);
        }
        private Subscription GetSubscription(dynamic subscription)
        {
            UserSubscription UserSubscription = GetUserSubscription(subscription);
            Procedure procedure = GetProcedure(subscription);
            Bundle bundle = GetBundle(subscription);
            var money = new Money((decimal)subscription.amount);
            var subscriptionSettings = new SubscriptionSettings((int)subscription.whatsappCounter,
                (int)subscription.emailCounter,
                (int)subscription.smsCounter,
                (int)subscription.totalWhatsapp,
                (int)subscription.totalEmail,
                (int)subscription.totalSms);

            return new Subscription((int)subscription.subscriptionId,
                subscription.subscriptionCode,
                UserSubscription,
                procedure,
                bundle,
                money,
                subscriptionSettings);
        }
        private UserProfile GetUserProfile(UserRequest request)
        {
            return new UserProfile(request.Civility,
                request.FirstName,
                request.LastName,
                request.PhoneNumber,
                request.Whatsapp,
                request.Email);
        }
        private Procedure GetProcedure(dynamic subscription)
        {
            var procedureType = new ProcedureType((int)subscription.procedureTypeId, 
                subscription.procedureTypeLabel);

            var organism = new Organism((int)subscription.organismId,
                    subscription.codeInsee,
                    subscription.organismName,
                    subscription.organismHref);

            return new Procedure(procedureType,
                organism,
                subscription.procedureHref);
        }
        private UserProfile GetUserProfile(dynamic user)
        {
            var userProfile = new UserProfile((Civility)user.civilityId,
                user.firstName,
                user.lastName,
                user.phoneNumber,
                user.whatsapp,
                user.email);

            userProfile.SetChannelConfirmationStatus((ChannelConfirmationStatus)user.channelConfirmationStatus);
            userProfile.SetChannelActivationStatus((ChannelActivationStatus)user.channelActivationStatus);
            return userProfile;
        }
        private Bundle GetBundle(dynamic subscription)
        {
            var bundleIdentifier = new BundleIdentifier((int)subscription.bundleId,
                subscription.bundleCode,
                subscription.bundleLabel);

            var bundleSettings = new BundleSettings((int)subscription.intervalInWeeks,
                (int)subscription.bundleTotalWhatsapp,
                (int)subscription.bundleTotalEmail,
                (int)subscription.bundleTotalSms);

            var priority = Priority.GetById((int)subscription.priority);
            var money = new Money((decimal)subscription.price);
            return new Bundle(bundleIdentifier,
                bundleSettings,
                priority,
                money);
        }

        #endregion
    }
}
