namespace Bizca.User.Domain.Entities.Address.BusinessCheck.Rules
{
    using Bizca.Core.Domain;
    using Bizca.Core.Domain.Country;
    using Bizca.Core.Domain.Exceptions;
    using Bizca.Core.Domain.Partner;
    using Bizca.User.Domain.Entities.Address.BusinessCheck.Exceptions;
    using System.Threading.Tasks;

    public sealed class IsAddressCountryMustExist : IAddressRule
    {
        private readonly ICountryRepository countryRepository;
        public IsAddressCountryMustExist(ICountryRepository countryRepository)
        {
            this.countryRepository = countryRepository;
        }
        public async Task<RuleResult> CheckAsync(AddressRequest request)
        {
            DomainFailure failure = null;
            bool success = (MandatoryAddressFlags.Country & request.Partner.Settings.FeatureFlags.MandatoryAddressFlags) == 0 ||
                            await countryRepository.GetByCodeAsync(request.Country).ConfigureAwait(false) != null;
            if (!success)
            {
                failure = new DomainFailure($"country is mandatory for partner::{request.Partner.PartnerCode}.",
                    nameof(request.Country),
                    typeof(AddressIsMandatoryException));
            }

            return await Task.FromResult(new RuleResult(failure is null, failure)).ConfigureAwait(false);
        }
    }
}