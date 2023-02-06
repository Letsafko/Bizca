namespace Bizca.User.Domain.BusinessCheck.UserRule
{
    using Agregates;
    using Contract;
    using Core.Domain.Referential.Enums;
    using Core.Domain.Rules;
    using System.Threading.Tasks;

    public sealed class IsUserAddressMandatory : IUserRule
    {
        public Task<CheckResult> CheckAsync(UserRequest request)
        {
            bool success =
                (MandatoryUserProfileField.Address & request.Partner.MandatoryUserProfileField) == 0 ||
                (!string.IsNullOrWhiteSpace(request.AddressCity) && !string.IsNullOrWhiteSpace(request.AddressCountry));

            CheckReport checkReport = null;
            if (!success)
                checkReport = new CheckReport("address is mandatory.", nameof(request.BirthCity));

            return Task.FromResult(new CheckResult(success, checkReport));
        }
    }
}