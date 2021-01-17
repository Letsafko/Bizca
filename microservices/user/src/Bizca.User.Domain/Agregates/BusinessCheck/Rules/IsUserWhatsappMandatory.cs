namespace Bizca.User.Domain.Agregates.BusinessCheck.Rules
{
    using Bizca.Core.Domain;
    using Bizca.Core.Domain.Exceptions;
    using Bizca.User.Domain.Agregates.BusinessCheck.Exceptions;
    using System.Threading.Tasks;

    public sealed class IsUserWhatsappMandatory : IUserRule
    {
        public async Task<RuleResult> CheckAsync(UserRequest request)
        {
            DomainFailure failure = null;
            bool succes = await Task.FromResult(!request.Partner.FeatureFlags.IsWhatsappMandatory || !string.IsNullOrWhiteSpace(request.Whatsapp)).ConfigureAwait(false);
            if (!succes)
            {
                failure = new DomainFailure($"whatsapp is mandatory for partner::{request.Partner.PartnerCode}.",
                    nameof(request.Whatsapp),
                    typeof(UserWhatsappMandatoryException));
            }
            return new RuleResult(succes, failure);
        }
    }
}