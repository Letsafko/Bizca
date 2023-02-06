namespace Bizca.User.Domain.BusinessCheck.UserRule
{
    using Agregates;
    using Contract;
    using Core.Domain.Referential.Enums;
    using Core.Domain.Rules;
    using System.Threading.Tasks;

    public sealed class IsUserPhoneMandatory : IUserRule
    {
        public Task<CheckResult> CheckAsync(UserRequest request)
        {
            bool success =
                (MandatoryUserProfileField.PhoneNumber & request.Partner.MandatoryUserProfileField) == 0 ||
                !string.IsNullOrWhiteSpace(request.PhoneNumber);

            CheckReport checkReport = null;
            if (!success)
                checkReport = new CheckReport("phone number is mandatory.", nameof(request.PhoneNumber));

            return Task.FromResult(new CheckResult(success, checkReport));
        }
    }
}