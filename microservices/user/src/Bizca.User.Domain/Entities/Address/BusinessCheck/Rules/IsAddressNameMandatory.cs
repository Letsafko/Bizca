namespace Bizca.User.Domain.Entities.Address.BusinessCheck.Rules
{
    using Bizca.Core.Domain;
    using Bizca.Core.Domain.Exceptions;
    using Bizca.Core.Domain.Partner;
    using Bizca.User.Domain.Entities.Address.BusinessCheck.Exceptions;
    using System.Threading.Tasks;

    public sealed class IsAddressNameMandatory : IAddressRule
    {
        public async Task<RuleResult> CheckAsync(AddressRequest request)
        {
            DomainFailure failure = null;
            bool success = (MandatoryAddressFlags.Name & request.Partner.Settings.FeatureFlags.MandatoryAddressFlags) == 0 || !string.IsNullOrWhiteSpace(request.Name);
            if(!success)
            {
                failure = new DomainFailure($"name is mandatory for partner::{request.Partner.PartnerCode}.",
                    nameof(request.Name),
                    typeof(AddressIsMandatoryException));
            }

            return await Task.FromResult(new RuleResult(failure is null, failure)).ConfigureAwait(false);
        }
    }
}