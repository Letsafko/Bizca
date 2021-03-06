namespace Bizca.User.Domain.Entities.Address.BusinessCheck.Rules
{
    using Bizca.Core.Domain;
    using Bizca.Core.Domain.Exceptions;
    using Bizca.Core.Domain.Partner;
    using Bizca.User.Domain.Entities.Address.BusinessCheck.Exceptions;
    using System.Threading.Tasks;

    public sealed class IsAddressStreetMandatory : IAddressRule
    {
        public async Task<RuleResult> CheckAsync(AddressRequest request)
        {
            DomainFailure failure = null;
            bool success = (MandatoryAddressFlags.Street & request.Partner.Settings.FeatureFlags.MandatoryAddressFlags) == 0 || !string.IsNullOrWhiteSpace(request.Street);
            if (!success)
            {
                failure = new DomainFailure($"street is mandatory for partner::{request.Partner.PartnerCode}.",
                    nameof(request.Street),
                    typeof(AddressIsMandatoryException));
            }

            return await Task.FromResult(new RuleResult(failure is null, failure)).ConfigureAwait(false);
        }
    }
}