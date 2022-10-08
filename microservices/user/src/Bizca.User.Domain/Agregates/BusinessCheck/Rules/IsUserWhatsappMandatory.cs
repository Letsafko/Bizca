namespace Bizca.User.Domain.Agregates.BusinessCheck.Rules
{
    using Core.Domain;
    using Core.Domain.Exceptions;
    using Core.Domain.Referential.Model;
    using Exceptions;
    using System.Threading.Tasks;

    public sealed class IsUserWhatsappMandatory : IUserRule
    {
        public async Task<RuleResult> CheckAsync(UserRequest request)
        {
            DomainFailure failure = null;
            bool succes =
                (MandatoryUserFlags.Whatsapp & request.Partner.Settings.FeatureFlags.MandatoryUserFlags) == 0 ||
                !string.IsNullOrWhiteSpace(request.Whatsapp);
            if (!succes)
                failure = new DomainFailure("whatsapp number is mandatory.",
                    nameof(request.Whatsapp),
                    typeof(WhatsappIsMandatoryException));
            return await Task.FromResult(new RuleResult(succes, failure)).ConfigureAwait(false);
        }
    }
}