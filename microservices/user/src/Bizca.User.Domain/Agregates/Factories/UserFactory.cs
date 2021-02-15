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
    using Bizca.User.Domain.Entities.Channel.ValueObjects;
    using System;
    using System.Collections.Generic;
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

            Civility civility = await referentialService.GetCivilityByIdAsync(request.Civility ?? 0).ConfigureAwait(false);
            Country birthCountry = await referentialService.GetCountryByCodeAsync(request.BirthCountry).ConfigureAwait(false);
            EconomicActivity economicActivity = await referentialService.GetEconomicActivityByIdAsync(request.EconomicActivity ?? 0).ConfigureAwait(false);

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
            (dynamic user, IEnumerable<dynamic> channels) = await userRepository.GetByIdAsync(request.Partner.Id, request.ExternalUserId).ConfigureAwait(false);
            if (user is null)
            {
                return UserNull.Instance;
            }

            UserBuilder builder = UserBuilder.Instance
                .WithPartner(request.Partner)
                .WithId(user.userId)
                .WithUserCode(new UserCode(user.userCode))
                .WithExternalUserId(new ExternalUserId(user.externalUserId))
                .WithLastName(!string.IsNullOrWhiteSpace(request.LastName) ? request.LastName : user.lastName)
                .WithFisrtName(!string.IsNullOrWhiteSpace(request.FirstName) ? request.FirstName : user.firstName)
                .WithBirthCity(!string.IsNullOrWhiteSpace(request.BirthCity) ? request.BirthCity : user.birthCity)
                .WithBirthDate(request.BirthDate ?? user.birthDate);

            if (!string.IsNullOrWhiteSpace(request.Email) && !request.Email.Equals(user.email, StringComparison.OrdinalIgnoreCase))
                builder.WithEmail(request.Email);
            else
                builder.WithEmail(user.email, user.emailActive, user.emailConfirmed);

            if (!string.IsNullOrWhiteSpace(request.PhoneNumber) && !request.PhoneNumber.Equals(user.phone, StringComparison.OrdinalIgnoreCase))
                builder.WithPhoneNumber(request.PhoneNumber);
            else
                builder.WithPhoneNumber(user.phone, user.phoneActive, user.phoneConfirmed);

            if (!string.IsNullOrWhiteSpace(request.Whatsapp) && !request.Whatsapp.Equals(user.whatsapp, StringComparison.OrdinalIgnoreCase))
                builder.WithWhatsapp(request.Whatsapp);
            else
                builder.WithWhatsapp(user.whatsapp, user.whatsappActive, user.whatsappConfirmed);

            Civility civility = await referentialService.GetCivilityByIdAsync(request.Civility ?? 0).ConfigureAwait(false);
            builder.WithCivility(civility ?? new Civility(user.civilityId, user.civilityCode));

            Country birthCountry = await referentialService.GetCountryByCodeAsync(request.BirthCountry).ConfigureAwait(false);
            builder.WithBirthCountry(birthCountry ?? new Country(user.birthCountryId, user.birthCountryCode, user.birthCountryDescription));

            EconomicActivity economicActivity = await referentialService.GetEconomicActivityByIdAsync(request.EconomicActivity ?? 0).ConfigureAwait(false);
            builder.WithEconomicActivity(economicActivity ?? new EconomicActivity(user.economicActivityId, user.economicActivityCode, user.economicActivityDescription));

            User result = builder.Build();
            foreach (dynamic channel in channels)
            {
                var channelType = ChannelType.GetById(channel.channelId);
                var channelCode = new ChannelConfirmation(channel.confirmationCode, channel.expirationDate);
                result.UpdateChannelCodeConfirmation(channelType, channelCode);
            }

            return result;
        }

        public async Task<IUser> BuildAsync(Partner partner, string externalUserId)
        {
            (dynamic user, IEnumerable<dynamic> channels) = await userRepository.GetByIdAsync(partner.Id, externalUserId).ConfigureAwait(false);
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
                .WithEmail(user.email, user.emailActive, user.emailConfirmed)
                .WithPhoneNumber(user.phone, user.phoneActive, user.phoneConfirmed)
                .WithWhatsapp(user.whatsapp, user.whatsappActive, user.whatsappConfirmed)
                .WithCivility(civility)
                .WithBirthCountry(country)
                .WithEconomicActivity(economicActivity)
                .Build() as IUser;

            foreach (dynamic channel in channels)
            {
                var channelType = ChannelType.GetById(channel.channelId);
                var channelCode = new ChannelConfirmation(channel.confirmationCode, channel.expirationDate);
                result.AddNewChannelCodeConfirmation(channelType, channelCode);
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