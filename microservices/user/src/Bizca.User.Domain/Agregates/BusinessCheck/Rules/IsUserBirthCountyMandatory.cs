namespace Bizca.User.Domain.Agregates.BusinessCheck.Rules
{
    using Bizca.Core.Domain;
    using Bizca.Core.Domain.Exceptions;
    using Bizca.Core.Domain.Partner;
    using Bizca.User.Domain.Agregates.BusinessCheck.Exceptions;
    using System.Threading.Tasks;

    public sealed class IsUserBirthCountyMandatory : IUserRule
    {
        public async Task<RuleResult> CheckAsync(UserRequest request)
        {
            DomainFailure failure = null;
            bool succes = (MandatoryUserFlags.BirthCounty & request.Partner.Settings.FeatureFlags.MandatoryUserFlags) == 0 || !string.IsNullOrWhiteSpace(request.BirthCountry);
            if (!succes)
            {
                failure = new DomainFailure("birth country is mandatory.",
                    nameof(request.BirthCountry),
                    typeof(BirthCountryIsMandatoryException));
            }
            return await Task.FromResult(new RuleResult(succes, failure)).ConfigureAwait(false);
        }
    }
}