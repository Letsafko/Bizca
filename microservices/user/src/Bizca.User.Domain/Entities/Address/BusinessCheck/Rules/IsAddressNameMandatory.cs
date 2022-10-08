namespace Bizca.User.Domain.Entities.Address.BusinessCheck.Rules
{
    using Core.Domain;
    using Core.Domain.Exceptions;
    using Core.Domain.Referential.Model;
    using Exceptions;
    using System.Threading.Tasks;

    public sealed class IsAddressNameMandatory : IAddressRule
    {
        public async Task<RuleResult> CheckAsync(AddressRequest request)
        {
            bool success =
                (MandatoryAddressFlags.Name & request.Partner.Settings.FeatureFlags.MandatoryAddressFlags) == 0 ||
                !string.IsNullOrWhiteSpace(request.Name);
            if (!success)
            {
                var failure = new DomainFailure("address name is mandatory.",
                    nameof(request.Name),
                    typeof(AddressNameIsMandatoryException));
                return new RuleResult(false, failure);
            }

            return await Task.FromResult(new RuleResult(true, null)).ConfigureAwait(false);
        }
    }
}