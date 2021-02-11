namespace Bizca.User.Domain.Agregates.BusinessCheck.Rules
{
    using Bizca.Core.Domain;
    using Bizca.Core.Domain.Exceptions;
    using Bizca.User.Domain.Agregates.BusinessCheck.Exceptions;
    using System.Threading.Tasks;

    public sealed class IsUserEconomicActivityMandotory : IUserRule
    {
        public async Task<RuleResult> CheckAsync(UserRequest request)
        {
            DomainFailure failure = null;
            bool succes = await Task.FromResult(!request.Partner.FeatureFlags.IsUserEconomicActivityMandotory || request.EconomicActivity.HasValue).ConfigureAwait(false);
            if (!succes)
            {
                failure = new DomainFailure($"economicActivity is mandatory for partner::{request.Partner.PartnerCode}.",
                    nameof(request.EconomicActivity),
                    typeof(UserEconomicActivityMandatoryException));
            }
            return new RuleResult(succes, failure);
        }
    }
}