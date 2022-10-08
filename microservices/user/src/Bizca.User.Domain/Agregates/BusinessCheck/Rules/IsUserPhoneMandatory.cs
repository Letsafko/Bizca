namespace Bizca.User.Domain.Agregates.BusinessCheck.Rules
{
    using Core.Domain;
    using Core.Domain.Exceptions;
    using Core.Domain.Referential.Model;
    using Exceptions;
    using System.Threading.Tasks;

    public sealed class IsUserPhoneMandatory : IUserRule
    {
        public async Task<RuleResult> CheckAsync(UserRequest request)
        {
            DomainFailure failure = null;
            bool succes =
                (MandatoryUserFlags.PhoneNumber & request.Partner.Settings.FeatureFlags.MandatoryUserFlags) == 0 ||
                !string.IsNullOrWhiteSpace(request.PhoneNumber);
            if (!succes)
                failure = new DomainFailure("phone number is mandatory.",
                    nameof(request.PhoneNumber),
                    typeof(PhoneNumberIsMandatoryException));
            return await Task.FromResult(new RuleResult(succes, failure)).ConfigureAwait(false);
        }
    }
}