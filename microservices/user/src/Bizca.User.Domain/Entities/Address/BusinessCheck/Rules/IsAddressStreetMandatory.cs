namespace Bizca.User.Domain.Entities.Address.BusinessCheck.Rules
{
    using Bizca.Core.Domain;
    using Bizca.Core.Domain.Exceptions;
    using Bizca.User.Domain.Entities.Address.BusinessCheck.Exceptions;
    using Core.Domain.Referential.Model;
    using System.Threading.Tasks;

    public sealed class IsAddressStreetMandatory : IAddressRule
    {
        public async Task<RuleResult> CheckAsync(AddressRequest request)
        {
            bool success = (MandatoryAddressFlags.Street & request.Partner.Settings.FeatureFlags.MandatoryAddressFlags) == 0 || !string.IsNullOrWhiteSpace(request.Street);
            if (!success)
            {
                var failure = new DomainFailure("address street is mandatory.",
                    nameof(request.Street),
                    typeof(AddressStreetIsMandatoryException));
                return new RuleResult(false, failure);
            }

            return await Task.FromResult(new RuleResult(true, null)).ConfigureAwait(false);
        }
    }
}