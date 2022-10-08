namespace Bizca.User.Domain.Entities.Address.BusinessCheck.Rules
{
    using Core.Domain;
    using Core.Domain.Exceptions;
    using Core.Domain.Referential.Model;
    using Exceptions;
    using System.Threading.Tasks;

    public sealed class IsAddressZipCodeMandatory : IAddressRule
    {
        public async Task<RuleResult> CheckAsync(AddressRequest request)
        {
            bool success =
                (MandatoryAddressFlags.ZipCode & request.Partner.Settings.FeatureFlags.MandatoryAddressFlags) == 0 ||
                !string.IsNullOrWhiteSpace(request.ZipCode);
            if (!success)
            {
                var failure = new DomainFailure("zipcode is mandatory.",
                    nameof(request.ZipCode),
                    typeof(AddressZipCodeIsMandatoryException));
                return new RuleResult(false, failure);
            }

            return await Task.FromResult(new RuleResult(true, null)).ConfigureAwait(false);
        }
    }
}