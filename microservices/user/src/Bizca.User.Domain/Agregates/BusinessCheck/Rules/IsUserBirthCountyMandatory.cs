namespace Bizca.User.Domain.Agregates.BusinessCheck.Rules
{
    using Bizca.Core.Domain;
    using Bizca.Core.Domain.Exceptions;
    using Bizca.User.Domain.Agregates.BusinessCheck.Exceptions;
    using System.Threading.Tasks;

    public sealed class IsUserBirthCountyMandatory : IUserRule
    {
        public async Task<RuleResult> CheckAsync(UserRequest request)
        {
            DomainFailure failure = null;
            bool succes = await Task.FromResult(!request.Partner.FeatureFlags.IsBirthCountyMandatory || !string.IsNullOrWhiteSpace(request.BirthCountry)).ConfigureAwait(false);
            if (!succes)
            {
                failure = new DomainFailure($"email is mandatory for partner::{request.Partner.PartnerCode}.",
                    nameof(request.Email),
                    typeof(UserEmailMandatoryException));
            }
            return new RuleResult(succes, failure);
        }
    }
}