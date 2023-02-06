namespace Bizca.User.Domain.BusinessCheck.AddressRule
{
    using Contract;
    using Core.Domain.Referential.Enums;
    using Core.Domain.Rules;
    using Entities.Address;
    using System.Threading.Tasks;

    public sealed class IsAddressNameMandatory : IAddressRule
    {
        public async Task<CheckResult> CheckAsync(AddressRequest request)
        {
            bool success =
                (MandatoryAddressField.Name & request.Partner.MandatoryAddressField) == 0 ||
                !string.IsNullOrWhiteSpace(request.Name);

            CheckReport checkReport = null;
            if (!success)
                checkReport = new CheckReport("address name is mandatory.", nameof(request.Name));

            return await Task.FromResult(new CheckResult(success, checkReport));
        }
    }
}