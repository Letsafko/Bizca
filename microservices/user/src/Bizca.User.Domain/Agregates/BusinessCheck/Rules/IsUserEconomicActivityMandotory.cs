namespace Bizca.User.Domain.Agregates.BusinessCheck.Rules
{
    using Core.Domain;
    using Core.Domain.Exceptions;
    using Core.Domain.Referential.Model;
    using Exceptions;
    using System.Threading.Tasks;

    public sealed class IsUserEconomicActivityMandotory : IUserRule
    {
        public async Task<RuleResult> CheckAsync(UserRequest request)
        {
            DomainFailure failure = null;
            bool succes =
                (MandatoryUserFlags.EconomicActivity & request.Partner.Settings.FeatureFlags.MandatoryUserFlags) == 0 ||
                request.EconomicActivity.HasValue;
            if (!succes)
                failure = new DomainFailure("economic activity is mandatory.",
                    nameof(request.EconomicActivity),
                    typeof(EconomicActivityIsMandatoryException));
            return await Task.FromResult(new RuleResult(succes, failure)).ConfigureAwait(false);
        }
    }
}