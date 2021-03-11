namespace Bizca.User.Domain.Agregates.Factories
{
    using Bizca.Core.Domain;
    using Bizca.Core.Domain.Civility;
    using Bizca.Core.Domain.Country;
    using Bizca.Core.Domain.EconomicActivity;
    using Bizca.Core.Domain.Exceptions;
    using Bizca.Core.Domain.Partner;
    using Bizca.Core.Domain.Services;
    using Bizca.User.Domain.Agregates.BusinessCheck;
    using Bizca.User.Domain.Agregates.Repositories;
    using Bizca.User.Domain.Agregates.ValueObjects;
    using Bizca.User.Domain.Entities.Channel;
    using Bizca.User.Domain.Entities.Channel.ValueObjects;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public sealed class UserFactory : IUserFactory
    {
        #region fields & ctor

        private readonly IReferentialService referentialService;
        private readonly IUserRuleEngine userRuleEngine;
        private readonly IUserRepository userRepository;
        public UserFactory(IUserRuleEngine userRuleEngine,
            IUserRepository userRepository,
            IReferentialService referentialService)
        {
            this.userRuleEngine = userRuleEngine;
            this.userRepository = userRepository;
            this.referentialService = referentialService;
        }

        #endregion

        public async Task<IUser> BuildByPartnerAndExternalUserIdAsync(Partner partner, string externalUserId)
        {
            Dictionary<ResultName, IEnumerable<dynamic>> resultDico = await userRepository.GetByPartnerIdAndExternalUserIdAsync(partner.Id, externalUserId).ConfigureAwait(false);
            return await ConstructUserAsync(partner, resultDico).ConfigureAwait(false);
        }
        public async Task<IUser> BuildByPartnerAndChannelResourceAsync(Partner partner, string channelResource)
        {
            Dictionary<ResultName, IEnumerable<dynamic>> resultDico = await userRepository.GetByPartnerIdAndChannelResourceAsync(partner.Id, channelResource).ConfigureAwait(false);
            return await ConstructUserAsync(partner, resultDico).ConfigureAwait(false);
        }
        public async Task<IUser> CreateAsync(UserRequest request)
        {
            RuleResultCollection collection = await userRuleEngine.CheckRulesAsync(request).ConfigureAwait(false);
            ManageResultChecks(collection);

            (EconomicActivity economicActivity, Civility civility, Country birthCountry) = await GetReferentialDataAsync(request.EconomicActivity,
                request.BirthCountry,
                request.Civility).ConfigureAwait(false);

            var userIdentifier = new UserIdentifier(0,
                new ExternalUserId(request.ExternalUserId),
                new UserCode(Guid.NewGuid()),
                request.Partner);

            var profile = new UserProfile(economicActivity,
                birthCountry,
                request.BirthDate,
                civility,
                request.BirthCity,
                request.FirstName,
                request.LastName);

            var user = new User(userIdentifier, profile);
            UpsertChannel(user, ChannelType.Whatsapp, request.Whatsapp);
            UpsertChannel(user, ChannelType.Sms, request.PhoneNumber);
            UpsertChannel(user, ChannelType.Email, request.Email);

            return user;
        }
        public async Task<IUser> UpdateAsync(UserRequest request)
        {
            if (!(await BuildByPartnerAndExternalUserIdAsync(request.Partner, request.ExternalUserId).ConfigureAwait(false) is User user))
            {
                return UserNull.Instance;
            }

            (EconomicActivity economicActivity, Civility civility, Country birthCountry) = await GetReferentialDataAsync(request.EconomicActivity,
                request.BirthCountry,
                request.Civility).ConfigureAwait(false);

            UpsertChannel(user, ChannelType.Whatsapp, request.Whatsapp);
            UpsertChannel(user, ChannelType.Sms, request.PhoneNumber);
            UpsertChannel(user, ChannelType.Email, request.Email);

            user.UpdateProfile(economicActivity,
                birthCountry,
                civility,
                request.BirthDate,
                request.BirthCity,
                request.LastName,
                request.FirstName);

            return user;
        }

        #region private helpers

        private async Task<(EconomicActivity economicActivity, Civility civility, Country birthCountry)> GetReferentialDataAsync(int? economicActivity, string birthCountry, int? civilityId)
        {
            var birthCountryTask = Task.FromResult<Country>(default);
            if (!string.IsNullOrWhiteSpace(birthCountry))
            {
                if (int.TryParse(birthCountry, out int birthCountryId))
                {
                    birthCountryTask = referentialService.GetCountryByIdAsync(birthCountryId);
                }
                else
                {
                    birthCountryTask = referentialService.GetCountryByCodeAsync(birthCountry);
                }
            }

            var economicActivityTask = Task.FromResult<EconomicActivity>(default);
            if (economicActivity.HasValue)
            {
                economicActivityTask = referentialService.GetEconomicActivityByIdAsync(economicActivity.Value);
            }

            var civilityTask = Task.FromResult<Civility>(default);
            if (civilityId.HasValue)
            {
                civilityTask = referentialService.GetCivilityByIdAsync(civilityId.Value, true);
            }

            await Task.WhenAll(birthCountryTask, civilityTask, economicActivityTask).ConfigureAwait(false);
            return
            (
                economicActivityTask.Result,
                civilityTask.Result,
                birthCountryTask.Result
            );
        }
        private async Task<IUser> ConstructUserAsync(Partner partner, Dictionary<ResultName, IEnumerable<dynamic>> resultDico)
        {
            dynamic result = resultDico[ResultName.User].FirstOrDefault();
            if (result is null)
            {
                return UserNull.Instance;
            }

            (EconomicActivity economicActivity, Civility civility, Country birthCountry) = await GetReferentialDataAsync((int?)result.economicActivityId,
                (string)result.birthCountryId?.ToString(),
                (int?)result.civilityId).ConfigureAwait(false);

            var userIdentifier = new UserIdentifier(result.userId,
                new ExternalUserId(result.externalUserId),
                new UserCode(result.userCode),
                partner);

            var profile = new UserProfile(economicActivity,
                birthCountry,
                result.birthDate,
                civility,
                result.birthCity,
                result.firstName,
                result.lastName);

            var user = new User(userIdentifier, profile);
            user.SetRowVersion(result.rowversion);

            if (!string.IsNullOrWhiteSpace(result.whatsapp))
            {
                user.AddChannel(result.whatsapp, ChannelType.Whatsapp, ConvertToBoolean(result.whatsappActive), ConvertToBoolean(result.whatsappConfirmed));
            }

            if (!string.IsNullOrWhiteSpace(result.email))
            {
                user.AddChannel(result.email, ChannelType.Email, ConvertToBoolean(result.emailActive), ConvertToBoolean(result.emailConfirmed));
            }

            if (!string.IsNullOrWhiteSpace(result.phone))
            {
                user.AddChannel(result.phone, ChannelType.Sms, ConvertToBoolean(result.phoneActive), ConvertToBoolean(result.phoneConfirmed));
            }

            foreach (dynamic channel in resultDico[ResultName.ChannelConfirmations])
            {
                var channelType = ChannelType.GetById(channel.channelId);
                var channelCode = new ChannelConfirmation(channel.confirmationCode, channel.expirationDate);
                user.AddChannelCodeConfirmation(channelType, channelCode);
            }

            foreach (dynamic addr in resultDico[ResultName.Addresses])
            {
                var ctr = new Country(addr.countryId,
                    addr.countryCode,
                    addr.description);

                user.BuildAddress(addr.addressId,
                    addr.active,
                    addr.street,
                    addr.city,
                    addr.zipcode,
                    ctr,
                    addr.name);
            }

            foreach (dynamic pwd in resultDico[ResultName.Passwords])
            {
                user.BuildPasword(pwd.active, pwd.passwordHash, pwd.securityStamp);
            }

            return user;
        }
        private void UpsertChannel(User user, ChannelType channelType, string channelValue)
        {
            if (!string.IsNullOrWhiteSpace(channelValue))
            {
                Channel channel = user.Profile.GetChannel(channelType, false);
                if (channel is null)
                {
                    user.AddChannel(channelValue, channelType, false, false);
                }
                else if (!channel.ChannelValue.Equals(channelValue, StringComparison.OrdinalIgnoreCase))
                {
                    user.UpdateChannel(channelValue, channelType, false, false);
                }
            }
        }
        private void ManageResultChecks(RuleResultCollection collection)
        {
            foreach (RuleResult rule in collection)
            {
                if (!rule.Sucess)
                {
                    throw Activator.CreateInstance(rule.Failure.ExceptionType, new List<DomainFailure> { rule.Failure }) as DomainException;
                }
            }
        }
        private bool ConvertToBoolean(int value)
        {
            return value == 1;
        }

        #endregion
    }
}