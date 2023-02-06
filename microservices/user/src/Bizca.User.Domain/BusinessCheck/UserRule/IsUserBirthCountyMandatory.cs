namespace Bizca.User.Domain.BusinessCheck.UserRule
{
    using Agregates;
    using Contract;
    using Core.Domain.Referential.Enums;
    using Core.Domain.Rules;
    using System.Threading.Tasks;

    public sealed class IsUserBirthCountyMandatory : IUserRule
    {
        public Task<CheckResult> CheckAsync(UserRequest request)
        {
            bool success =
                (MandatoryUserProfileField.BirthCounty & request.Partner.MandatoryUserProfileField) == 0 ||
                !string.IsNullOrWhiteSpace(request.BirthCountry);

            CheckReport checkReport = null;
            if (!success)
                checkReport = new CheckReport("birth country is mandatory.",
                    nameof(request.BirthCountry));

            return Task.FromResult(new CheckResult(success, checkReport));
        }
    }
}