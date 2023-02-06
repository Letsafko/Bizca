namespace Bizca.User.Domain.BusinessCheck.UserRule
{
    using Agregates;
    using Contract;
    using Core.Domain.Referential.Enums;
    using Core.Domain.Rules;
    using System.Threading.Tasks;

    public sealed class IsUserEconomicActivityMandatory : IUserRule
    {
        public async Task<CheckResult> CheckAsync(UserRequest request)
        {
            bool success =
                (MandatoryUserProfileField.EconomicActivity & request.Partner.MandatoryUserProfileField) == 0 ||
                request.EconomicActivity.HasValue;

            CheckReport checkReport = null;
            if (!success)
                checkReport = new CheckReport("economic activity is mandatory.",
                    nameof(request.EconomicActivity));

            return await Task.FromResult(new CheckResult(success, checkReport)).ConfigureAwait(false);
        }
    }
}