namespace Bizca.User.Domain.BusinessCheck.AddressRule
{
    using Contract;
    using Core.Domain.Referential.Enums;
    using Core.Domain.Rules;
    using Entities.Address;
    using System.Threading.Tasks;

    public sealed class IsAddressZipCodeMandatory : IAddressRule
    {
        public async Task<CheckResult> CheckAsync(AddressRequest request)
        {
            bool success =
                (MandatoryAddressField.ZipCode & request.Partner.MandatoryAddressField) == 0 ||
                !string.IsNullOrWhiteSpace(request.ZipCode);

            CheckReport checkReport = null;
            if (!success)
                checkReport = new CheckReport("zipcode is mandatory.", nameof(request.Street));

            return await Task.FromResult(new CheckResult(success, checkReport));
        }
    }
}