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

        private readonly IUserRuleEngine userRuleEngine;
        private readonly IUserRepository userRepository;
        private readonly IReferentialService referentialService;
        public UserFactory(IUserRuleEngine userRuleEngine,
            IUserRepository userRepository,
            IReferentialService referentialService)
        {
            this.userRuleEngine = userRuleEngine;
            this.userRepository = userRepository;
            this.referentialService = referentialService;
        }

        #endregion

        public async Task<IUser> CreateAsync(UserRequest request)
        {
            RuleResultCollection collection = await userRuleEngine.CheckRulesAsync(request).ConfigureAwait(false);
            ManageResultChecks(collection);

            Civility civility = await referentialService.GetCivilityByIdAsync(request.Civility.Value).ConfigureAwait(false);

            Country birthCountry = null;
            if (!string.IsNullOrWhiteSpace(request.BirthCountry))
            {
                birthCountry = await referentialService.GetCountryByCodeAsync(request.BirthCountry).ConfigureAwait(false);
            }

            EconomicActivity economicActivity = null;
            if (request.EconomicActivity.HasValue)
            {
                economicActivity = await referentialService.GetEconomicActivityByIdAsync(request.EconomicActivity.Value).ConfigureAwait(false);
            }

            return UserBuilder.Instance
                    .WithUserCode(new UserCode(Guid.NewGuid()))
                    .WithPartner(request.Partner)
                    .WithExternalUserId(new ExternalUserId(request.ExternalUserId))
                    .WithCivility(civility)
                    .WithBirthCountry(birthCountry)
                    .WithEconomicActivity(economicActivity)
                    .WithBirthCity(request.BirthCity)
                    .WithBirthDate(request.BirthDate.Value)
                    .WithEmail(request.Email)
                    .WithPhoneNumber(request.PhoneNumber)
                    .WithFisrtName(request.FirstName)
                    .WithLastName(request.LastName)
                    .WithWhatsapp(request.Whatsapp)
                    .Build();
        }

        public async Task<IUser> UpdateAsync(UserRequest request)
        {
            if (!(await BuildAsync(request.Partner, request.ExternalUserId).ConfigureAwait(false) is User user))
            {
                return UserNull.Instance;
            }

            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                Channel channel = user.GetChannel(ChannelType.Email, false);
                if(channel is null)
                {
                    user.AddChannel(request.Email, ChannelType.Email, false, false);
                }
                else if (!channel.ChannelValue.Equals(request.Email, StringComparison.OrdinalIgnoreCase))
                {
                    channel.UpdateChannel(request.Email, false, false);
                }
            }

            if (!string.IsNullOrWhiteSpace(request.Whatsapp))
            {
                Channel channel = user.GetChannel(ChannelType.Whatsapp, false);
                if (channel is null)
                {
                    user.AddChannel(request.Whatsapp, ChannelType.Whatsapp, false, false);
                }
                else if (!channel.ChannelValue.Equals(request.Whatsapp, StringComparison.OrdinalIgnoreCase))
                {
                    channel.UpdateChannel(request.Whatsapp, false, false);
                }
            }

            if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
            {
                Channel channel = user.GetChannel(ChannelType.Sms, false);
                if (channel is null)
                {
                    user.AddChannel(request.PhoneNumber, ChannelType.Sms, false, false);
                }
                else if (!channel.ChannelValue.Equals(request.PhoneNumber, StringComparison.OrdinalIgnoreCase))
                {
                    channel.UpdateChannel(request.PhoneNumber, false, false);
                }
            }

            Civility civility = null;
            if (request.Civility.HasValue)
            {
                civility = await referentialService.GetCivilityByIdAsync(request.Civility.Value).ConfigureAwait(false);
            }

            Country birthCountry = null;
            if (!string.IsNullOrWhiteSpace(request.BirthCountry))
            {
                birthCountry = await referentialService.GetCountryByCodeAsync(request.BirthCountry).ConfigureAwait(false);
            }

            EconomicActivity economicActivity = null;
            if (request.EconomicActivity.HasValue)
            {
                economicActivity = await referentialService.GetEconomicActivityByIdAsync(request.EconomicActivity.Value).ConfigureAwait(false);
            }

            return UserBuilder.Create(user)
                .WithFisrtName(!string.IsNullOrWhiteSpace(request.FirstName) ? request.FirstName : user.FirstName)
                .WithBirthCity(!string.IsNullOrWhiteSpace(request.BirthCity) ? request.BirthCity : user.BirthCity)
                .WithLastName(!string.IsNullOrWhiteSpace(request.LastName) ? request.LastName : user.LastName)
                .WithEconomicActivity(economicActivity ?? user.EconomicActivity)
                .WithBirthCountry(birthCountry ?? user.BirthCountry)
                .WithBirthDate(request.BirthDate ?? user.BirthDate)
                .WithCivility(civility ?? user.Civility)
                .Build();
        }

        public async Task<IUser> BuildAsync(Partner partner, string externalUserId)
        {
            Dictionary<ResultName, IEnumerable<dynamic>> resultDico = await userRepository.GetByIdAsync(partner.Id, externalUserId).ConfigureAwait(false);
            dynamic user = resultDico[ResultName.User].FirstOrDefault();
            if (user is null)
            {
                return UserNull.Instance;
            }

            Country country = await referentialService.GetCountryByIdAsync(user.birthCountryId ?? 0);
            Civility civility = await referentialService.GetCivilityByIdAsync(user.civilityId, true);
            EconomicActivity economicActivity = await referentialService.GetEconomicActivityByIdAsync(user.economicActivityId ?? 0);

            var result = UserBuilder.Instance
                .WithPartner(partner)
                .WithId(user.userId)
                .WithUserCode(new UserCode(user.userCode))
                .WithExternalUserId(new ExternalUserId(user.externalUserId))
                .WithLastName(user.lastName)
                .WithFisrtName(user.firstName)
                .WithBirthCity(user.birthCity)
                .WithBirthDate(user.birthDate)
                .WithRowVersion(user.rowversion)
                .WithEmail(user.email, user.emailActive, user.emailConfirmed)
                .WithPhoneNumber(user.phone, user.phoneActive, user.phoneConfirmed)
                .WithWhatsapp(user.whatsapp, user.whatsappActive, user.whatsappConfirmed)
                .WithCivility(civility)
                .WithBirthCountry(country)
                .WithEconomicActivity(economicActivity)
                .Build() as IUser;

            foreach (dynamic channel in resultDico[ResultName.ChannelConfirmations])
            {
                var channelType = ChannelType.GetById(channel.channelId);
                var channelCode = new ChannelConfirmation(channel.confirmationCode, channel.expirationDate);
                result.AddNewChannelCodeConfirmation(channelType, channelCode);
            }

            foreach (dynamic pwd in resultDico[ResultName.Passwords])
            {
                result.BuildPassword(pwd.active, pwd.passwordHash, pwd.securityStamp);
            }

            foreach (dynamic addr in resultDico[ResultName.Addresses])
            {
                var ctr = new Country(addr.countryId, addr.countryCode, addr.description);
                result.BuildAddress(addr.addressId, addr.active, addr.street, addr.city, addr.zipcode, ctr, addr.name);
            }

            return result;
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
    }
}