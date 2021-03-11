namespace Bizca.User.Domain.Agregates.BusinessCheck.Rules
{
    using Bizca.Core.Domain;
    using Bizca.Core.Domain.Exceptions;
    using Bizca.Core.Domain.Partner;
    using Bizca.User.Domain.Agregates.BusinessCheck.Exceptions;
    using System.Threading.Tasks;

    public sealed class IsUserEmailMandatory : IUserRule
    {
        public async Task<RuleResult> CheckAsync(UserRequest request)
        {
            DomainFailure failure = null;
            bool succes = (MandatoryUserFlags.Email & request.Partner.Settings.FeatureFlags.MandatoryUserFlags) == 0 || !string.IsNullOrWhiteSpace(request.Email);
            if (!succes)
            {
                failure = new DomainFailure("email is mandatory.",
                    nameof(request.Email),
                    typeof(EmailIsMandatoryException));
            }
            return await Task.FromResult(new RuleResult(succes, failure)).ConfigureAwait(false);
        }
    }
}