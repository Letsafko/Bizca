namespace Bizca.Core.Domain.Referential.Services
{
    using Exceptions;
    using Model;
    using Repository;
    using System.Threading.Tasks;

    public sealed class ReferentialService : IReferentialService
    {
        private readonly IEconomicActivityRepository _economicActivityRepository;
        private readonly IEmailTemplateRepository _emailTemplateRepository;
        private readonly ICivilityRepository _civilityRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IPartnerRepository _partnerRepository;

        public ReferentialService(ICivilityRepository civilityRepository,
            IEconomicActivityRepository economicActivityRepository,
            IEmailTemplateRepository emailTemplateRepository,
            ICountryRepository countryRepository,
            IPartnerRepository partnerRepository)
        {
            _economicActivityRepository = economicActivityRepository;
            _emailTemplateRepository = emailTemplateRepository;
            _civilityRepository = civilityRepository;
            _countryRepository = countryRepository;
            _partnerRepository = partnerRepository;
        }

        public async Task<EconomicActivity> GetEconomicActivityByIdAsync(int economicActivity, bool throwOnError = false)
        {
            return await _economicActivityRepository.GetByIdAsync(economicActivity)
                   ?? (!throwOnError
                       ? default(EconomicActivity)
                       : throw BuildException(nameof(economicActivity),
                           economicActivity));
        }
        
        public async Task<EmailTemplate> GetEmailTemplateByIdAsync(int emailTemplate, bool throwOnError = false)
        {
            return await _emailTemplateRepository.GetByIdAsync(emailTemplate)
                   ?? (!throwOnError
                       ? default(EmailTemplate)
                       : throw BuildException(nameof(emailTemplate),
                           emailTemplate));
        }

        public async Task<Country> GetCountryByCodeAsync(string country, bool throwOnError = false)
        {
            return await _countryRepository.GetByCodeAsync(country)
                   ?? (!throwOnError
                       ? default(Country)
                       : throw BuildException(nameof(country), country));
        }

        public async Task<Partner> GetPartnerByCodeAsync(string partner, bool throwOnError = false)
        {
            return await _partnerRepository.GetByCodeAsync(partner)
                   ?? (!throwOnError
                       ? default(Partner)
                       : throw BuildException(nameof(partner), partner));
        }

        public async Task<Country> GetCountryByIdAsync(int countryId, bool throwOnError = false)
        {
            return await _countryRepository.GetByIdAsync(countryId)
                   ?? (!throwOnError
                       ? default(Country)
                       : throw BuildException(nameof(countryId), countryId));
        }
        
        public async Task<Civility> GetCivilityByIdAsync(int civility, bool throwOnError = false)
        {
            return await _civilityRepository.GetByIdAsync(civility)
                   ?? (!throwOnError
                       ? default(Civility)
                       : throw BuildException(nameof(civility), civility));
        }

        private static ResourceNotFoundException BuildException(string propertyName, object propertyValue)
        {
            return new ResourceNotFoundException($"given [{propertyName}={propertyValue}] does not exist.");
        }
    }
}