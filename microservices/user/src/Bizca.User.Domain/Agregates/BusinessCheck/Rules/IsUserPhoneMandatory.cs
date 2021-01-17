namespace Bizca.User.Domain.Agregates.BusinessCheck.Rules
{
    using Bizca.Core.Domain;
    using Bizca.Core.Domain.Exceptions;
    using Bizca.User.Domain.Agregates.BusinessCheck.Exceptions;
    using System.Threading.Tasks;

    public sealed class IsUserPhoneMandatory : IUserRule
    {
        public async Task<RuleResult> CheckAsync(UserRequest request)
        {
            DomainFailure failure = null;
            bool succes = await Task.FromResult(!request.Partner.FeatureFlags.IsPhoneMandatory || !string.IsNullOrWhiteSpace(request.PhoneNumber)).ConfigureAwait(false);
            if (!succes)
            {
                failure = new DomainFailure($"phone is mandatory for partner::{request.Partner.PartnerCode}.",
                    nameof(request.PhoneNumber),
                    typeof(UserPhoneMandatoryException));
            }
            return new RuleResult(succes, failure);
        }
    }
}