﻿namespace Bizca.User.Domain.Agregates.Factories
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

            Civility civility = await referentialService.GetCivilityByIdAsync(request.Civility).ConfigureAwait(false);
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
            dynamic result = await userRepository.GetByIdAsync(request.Partner.Id, request.ExternalUserId).ConfigureAwait(false);
            if (result is null)
            {
                return UserNull.Instance;
            }

            UserBuilder builder = UserBuilder.Instance
                .WithPartner(request.Partner)
                .WithId(result.userId)
                .WithUserCode(new UserCode(result.userCode))
                .WithExternalUserId(new ExternalUserId(result.externalUserId))
                .WithLastName(!string.IsNullOrWhiteSpace(request.LastName) ? request.LastName : result.lastName)
                .WithFisrtName(!string.IsNullOrWhiteSpace(request.FirstName) ? request.FirstName : result.firstName)
                .WithBirthCity(!string.IsNullOrWhiteSpace(request.BirthCity) ? request.BirthCity : result.birthCity)
                .WithBirthDate(request.BirthDate ?? result.birthDate);

            if (!string.IsNullOrWhiteSpace(request.Email) && !request.Email.Equals(result.email, StringComparison.OrdinalIgnoreCase))
                builder.WithEmail(request.Email);
            else
                builder.WithEmail(result.email, result.emailActive, result.emailConfirmed);

            if (!string.IsNullOrWhiteSpace(request.PhoneNumber) && !request.PhoneNumber.Equals(result.phone, StringComparison.OrdinalIgnoreCase))
                builder.WithPhoneNumber(request.PhoneNumber);
            else
                builder.WithPhoneNumber(result.phone, result.phoneActive, result.phoneConfirmed);

            if (!string.IsNullOrWhiteSpace(request.Whatsapp) && !request.Whatsapp.Equals(result.whatsapp, StringComparison.OrdinalIgnoreCase))
                builder.WithWhatsapp(request.Whatsapp);
            else
                builder.WithWhatsapp(result.whatsapp, result.whatsappActive, result.whatsappConfirmed);

            Civility civility = await referentialService.GetCivilityByIdAsync(request.Civility).ConfigureAwait(false);
            builder.WithCivility(civility ?? new Civility(result.civilityId, result.civilityCode));

            Country birthCountry = await referentialService.GetCountryByCodeAsync(request.BirthCountry).ConfigureAwait(false);
            builder.WithBirthCountry(birthCountry ?? new Country(result.birthCountryId, result.birthCountryCode, result.birthCountryDescription));

            EconomicActivity economicActivity = await referentialService.GetEconomicActivityByIdAsync(request.EconomicActivity ?? 0).ConfigureAwait(false);
            builder.WithEconomicActivity(economicActivity ?? new EconomicActivity(result.economicActivityId, result.economicActivityCode, result.economicActivityDescription));

            return builder.Build();
        }

        public async Task<IUser> BuildAsync(Partner partner, string externalUserId)
        {
            dynamic result = await userRepository.GetByIdAsync(partner.Id, externalUserId).ConfigureAwait(false);
            return result is null
                ? UserNull.Instance
                : UserBuilder.Instance
                     .WithPartner(partner)
                     .WithId(result.userId)
                     .WithUserCode(new UserCode(result.userCode))
                     .WithExternalUserId(new ExternalUserId(result.externalUserId))
                     .WithLastName(result.lastName)
                     .WithFisrtName(result.firstName)
                     .WithBirthCity(result.birthCity)
                     .WithBirthDate(result.birthDate)
                     .WithEmail(result.email, result.emailActive, result.emailConfirmed)
                     .WithPhoneNumber(result.phone, result.phoneActive, result.phoneConfirmed)
                     .WithWhatsapp(result.whatsapp, result.whatsappActive, result.whatsappConfirmed)
                     .WithCivility(new Civility(result.civilityId, result.civilityCode))
                     .WithBirthCountry(new Country(result.birthCountryId, result.birthCountryCode, result.birthCountryDescription))
                     .WithEconomicActivity(new EconomicActivity(result.economicActivityId, result.economicActivityCode, result.economicActivityDescription))
                     .Build() as IUser;
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