namespace Bizca.User.Domain.Agregates.BusinessCheck.Rules
{
    using Core.Domain;
    using Core.Domain.Exceptions;
    using Core.Domain.Referential.Model;
    using Exceptions;
    using System.Threading.Tasks;

    public sealed class IsUserAddressMandatory : IUserRule
    {
        public async Task<RuleResult> CheckAsync(UserRequest request)
        {
            DomainFailure failure = null;
            bool succes =
                (MandatoryUserFlags.Address & request.Partner.Settings.FeatureFlags.MandatoryUserFlags) == 0 ||
                (!string.IsNullOrWhiteSpace(request.AddressCity) && !string.IsNullOrWhiteSpace(request.AddressCountry));

            if (!succes)
                failure = new DomainFailure("address is mandatory.",
                    nameof(request.BirthCity),
                    typeof(AddressIsMandatoryException));
            return await Task.FromResult(new RuleResult(succes, failure)).ConfigureAwait(false);
        }
    }
}