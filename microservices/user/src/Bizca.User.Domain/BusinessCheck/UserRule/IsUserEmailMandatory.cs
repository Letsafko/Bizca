namespace Bizca.User.Domain.BusinessCheck.UserRule
{
    using Agregates;
    using Contract;
    using Core.Domain.Referential.Enums;
    using Core.Domain.Rules;
    using System.Threading.Tasks;

    public sealed class IsUserEmailMandatory : IUserRule
    {
        public Task<CheckResult> CheckAsync(UserRequest request)
        {
            bool success =
                (MandatoryUserProfileField.Email & request.Partner.MandatoryUserProfileField) == 0 ||
                !string.IsNullOrWhiteSpace(request.Email);

            CheckReport checkReport = null;
            if (!success)
                checkReport = new CheckReport("email is mandatory.", nameof(request.Email));

            return Task.FromResult(new CheckResult(success, checkReport));
        }
    }
}