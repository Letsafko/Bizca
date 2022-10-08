namespace Bizca.User.Domain.Agregates.BusinessCheck.Rules
{
    using Core.Domain;
    using Core.Domain.Exceptions;
    using Core.Domain.Referential.Model;
    using Exceptions;
    using System.Threading.Tasks;

    public sealed class IsUserBirthDateMandatory : IUserRule
    {
        public async Task<RuleResult> CheckAsync(UserRequest request)
        {
            DomainFailure failure = null;
            bool succes =
                (MandatoryUserFlags.BirthDate & request.Partner.Settings.FeatureFlags.MandatoryUserFlags) == 0 ||
                !request.BirthDate.HasValue;
            if (!succes)
                failure = new DomainFailure("birth date is mandatory.",
                    nameof(request.BirthDate),
                    typeof(BirthDateIsMandatoryException));
            return await Task.FromResult(new RuleResult(succes, failure)).ConfigureAwait(false);
        }
    }
}