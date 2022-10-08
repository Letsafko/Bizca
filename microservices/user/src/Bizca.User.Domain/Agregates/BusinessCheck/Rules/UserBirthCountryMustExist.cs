﻿namespace Bizca.User.Domain.Agregates.BusinessCheck.Rules
{
    using Core.Domain;
    using Core.Domain.Exceptions;
    using Core.Domain.Referential.Exception;
    using Core.Domain.Referential.Model;
    using Core.Domain.Referential.Repository;
    using System.Threading.Tasks;

    public sealed class UserBirthCountryMustExist : IUserRule
    {
        private readonly ICountryRepository countryRepository;

        public UserBirthCountryMustExist(ICountryRepository countryRepository)
        {
            this.countryRepository = countryRepository;
        }

        public async Task<RuleResult> CheckAsync(UserRequest request)
        {
            DomainFailure failure = null;
            bool succes = (MandatoryUserFlags.BirthCounty & request.Partner.Settings.FeatureFlags.MandatoryUserFlags) ==
                          0 ||
                          await countryRepository.GetByCodeAsync(request.BirthCountry).ConfigureAwait(false) != null;
            if (!succes)
                failure = new DomainFailure($"birthCountry::{request.BirthCountry} does not exist.",
                    nameof(request.BirthCountry),
                    typeof(CountryDoesNotExistException));
            return new RuleResult(succes, failure);
        }
    }
}