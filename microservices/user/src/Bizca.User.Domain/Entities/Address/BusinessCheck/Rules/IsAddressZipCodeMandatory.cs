namespace Bizca.User.Domain.Entities.Address.BusinessCheck.Rules
{
    using Bizca.Core.Domain;
    using Bizca.Core.Domain.Exceptions;
    using Bizca.Core.Domain.Partner;
    using Bizca.User.Domain.Entities.Address.BusinessCheck.Exceptions;
    using System.Threading.Tasks;

    public sealed class IsAddressZipCodeMandatory : IAddressRule
    {
        public async Task<RuleResult> CheckAsync(AddressRequest request)
        {
            DomainFailure failure = null;
            bool success = (MandatoryAddressFlags.ZipCode & request.Partner.Settings.FeatureFlags.MandatoryAddressFlags) == 0 || !string.IsNullOrWhiteSpace(request.ZipCode);
            if(!success)
            {
                failure = new DomainFailure($"zipcode is mandatory for partner::{request.Partner.PartnerCode}.",
                    nameof(request.ZipCode),
                    typeof(AddressIsMandatoryException));
            }

            return await Task.FromResult(new RuleResult(failure is null, failure)).ConfigureAwait(false);
        }
    }
}