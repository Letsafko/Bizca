namespace Bizca.User.Domain.Agregates.BusinessCheck.Rules
{
    using Bizca.Core.Domain;
    using Bizca.Core.Domain.Exceptions;
    using Bizca.Core.Domain.Partner;
    using Bizca.User.Domain.Agregates.BusinessCheck.Exceptions;
    using System.Threading.Tasks;

    public sealed class IsUserWhatsappMandatory : IUserRule
    {
        public async Task<RuleResult> CheckAsync(UserRequest request)
        {
            DomainFailure failure = null;
            bool succes = (MandatoryUserFlags.Whatsapp & request.Partner.Settings.FeatureFlags.MandatoryUserFlags) == 0 || !string.IsNullOrWhiteSpace(request.Whatsapp);
            if (!succes)
            {
                failure = new DomainFailure($"whatsapp is mandatory for partner::{request.Partner.PartnerCode}.",
                    nameof(request.Whatsapp),
                    typeof(UserWhatsappMandatoryException));
            }
            return await Task.FromResult(new RuleResult(succes, failure));
        }
    }
}