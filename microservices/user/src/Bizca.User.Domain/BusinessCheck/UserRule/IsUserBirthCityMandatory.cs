namespace Bizca.User.Domain.BusinessCheck.UserRule
{
    using Agregates;
    using Contract;
    using Core.Domain.Referential.Enums;
    using Core.Domain.Rules;
    using System.Threading.Tasks;

    public sealed class IsUserBirthCityMandatory : IUserRule
    {
        public Task<CheckResult> CheckAsync(UserRequest request)
        {
            bool success =
                (MandatoryUserProfileField.BirthCity & request.Partner.MandatoryUserProfileField) == 0 ||
                !string.IsNullOrWhiteSpace(request.BirthCity);

            CheckReport checkReport = null;
            if (!success)
                checkReport = new CheckReport("birth city is mandatory.", nameof(request.BirthCity));

            return Task.FromResult(new CheckResult(success, checkReport));
        }
    }
}