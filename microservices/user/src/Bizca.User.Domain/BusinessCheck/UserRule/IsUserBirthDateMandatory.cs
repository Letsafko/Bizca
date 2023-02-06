namespace Bizca.User.Domain.BusinessCheck.UserRule
{
    using Agregates;
    using Contract;
    using Core.Domain.Referential.Enums;
    using Core.Domain.Rules;
    using System.Threading.Tasks;

    public sealed class IsUserBirthDateMandatory : IUserRule
    {
        public Task<CheckResult> CheckAsync(UserRequest request)
        {
            bool success =
                (MandatoryUserProfileField.BirthDate & request.Partner.MandatoryUserProfileField) == 0 ||
                !request.BirthDate.HasValue;

            CheckReport checkReport = null;
            if (!success)
                checkReport = new CheckReport("birth date is mandatory.", nameof(request.BirthDate));

            return Task.FromResult(new CheckResult(success, checkReport));
        }
    }
}