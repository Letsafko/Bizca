namespace Bizca.User.Domain.Agregates.BusinessCheck.Rules
{
    using Bizca.Core.Domain;
    using Bizca.Core.Domain.Exceptions;
    using Bizca.Core.Domain.Partner;
    using Bizca.User.Domain.Agregates.BusinessCheck.Exceptions;
    using System.Threading.Tasks;

    public sealed class IsUserEconomicActivityMandotory : IUserRule
    {
        public async Task<RuleResult> CheckAsync(UserRequest request)
        {
            DomainFailure failure = null;
            bool succes = (MandatoryUserFlags.EconomicActivity & request.Partner.Settings.FeatureFlags.MandatoryUserFlags) == 0 || request.EconomicActivity.HasValue;
            if (!succes)
            {
                failure = new DomainFailure($"economicActivity is mandatory for partner::{request.Partner.PartnerCode}.",
                    nameof(request.EconomicActivity),
                    typeof(UserEconomicActivityMandatoryException));
            }
            return await Task.FromResult(new RuleResult(succes, failure)).ConfigureAwait(false);
        }
    }
}