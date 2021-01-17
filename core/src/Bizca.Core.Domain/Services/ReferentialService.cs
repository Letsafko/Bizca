namespace Bizca.Core.Domain.Services
{
    using Bizca.Core.Domain.Civility;
    using Bizca.Core.Domain.Country;
    using Bizca.Core.Domain.EconomicActivity;
    using Bizca.Core.Domain.Exceptions;
    using Bizca.Core.Domain.Partner;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    ///     Service to retrieve referential data.
    /// </summary>
    public sealed class ReferentialService : IReferentialService
    {
        private readonly ICountryRepository countryRepository;
        private readonly IPartnerRepository partnerRepository;
        private readonly ICivilityRepository civilityRepository;
        private readonly IEconomicActivityRepository economicActivityRepository;
        public ReferentialService(ICivilityRepository civilityRepository,
            ICountryRepository countryRepository,
            IPartnerRepository partnerRepository,
            IEconomicActivityRepository economicActivityRepository)
        {
            this.countryRepository = countryRepository;
            this.partnerRepository = partnerRepository;
            this.civilityRepository = civilityRepository;
            this.economicActivityRepository = economicActivityRepository;
        }

        /// <summary>
        ///     Gets civility.
        /// </summary>
        /// <param name="civility">civility identifier.</param>
        /// <param name="throwError">when true and result is null, an exception is thrown.</param>
        public async Task<Civility> GetCivilityByIdAsync(int civility, bool throwError = false)
        {
            return await civilityRepository.GetByIdAsync(civility).ConfigureAwait(false)
                ?? (!throwError ? default(Civility) : throw GetDomainException<CivilityDoesNotExistException>(nameof(civility), civility));
        }

        /// <summary>
        ///     Gets country.
        /// </summary>
        /// <param name="country">country code.</param>
        /// <param name="throwError">when true and result is null, an exception is thrown.</param>
        public async Task<Country> GetCountryByCodeAsync(string country, bool throwError = false)
        {
            return await countryRepository.GetByCodeAsync(country).ConfigureAwait(false)
                ?? (!throwError ? default(Country) : throw GetDomainException<CountryDoesNotExistException>(nameof(country), country));
        }

        /// <summary>
        ///     Gets partner.
        /// </summary>
        /// <param name="partner">partner code.</param>
        /// <param name="throwError">when true and result is null, an exception is thrown.</param>
        public async Task<Partner> GetPartnerByCodeAsync(string partner, bool throwError = false)
        {
            return await partnerRepository.GetByCodeAsync(partner).ConfigureAwait(false)
                ?? (!throwError ? default(Partner) : throw GetDomainException<PartnerDoesNotExistException>(nameof(partner), partner));
        }

        /// <summary>
        ///     Gets economic activity.
        /// </summary>
        /// <param name="economicActivity">economic activity identifier</param>
        /// <param name="throwError">when true and result is null, an exception is thrown.</param>
        public async Task<EconomicActivity> GetEconomicActivityByIdAsync(int economicActivity, bool throwError = false)
        {
            return await economicActivityRepository.GetByIdAsync(economicActivity).ConfigureAwait(false)
                ?? (!throwError ? default(EconomicActivity) : throw GetDomainException<EconomicActivityDoesNotExistException>(nameof(economicActivity), economicActivity));
        }

        private TException GetDomainException<TException>(string propertyName, object propertyValue) where TException : DomainException
        {
            var failure = new DomainFailure($"{propertyValue} does not exist.",
                    propertyName,
                    typeof(TException));

            return Activator.CreateInstance(typeof(TException), new List<DomainFailure> { failure }) as TException;
        }
    }
}