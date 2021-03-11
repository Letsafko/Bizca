namespace Bizca.User.Domain.Agregates.BusinessCheck.Rules
{
    using Bizca.Core.Domain;
    using Bizca.Core.Domain.Exceptions;
    using Bizca.Core.Domain.Partner;
    using Bizca.User.Domain.Agregates.BusinessCheck.Exceptions;
    using System.Threading.Tasks;

    public sealed class IsUserBirthDateMandatory : IUserRule
    {
        public async Task<RuleResult> CheckAsync(UserRequest request)
        {
            DomainFailure failure = null;
            bool succes = (MandatoryUserFlags.BirthDate & request.Partner.Settings.FeatureFlags.MandatoryUserFlags) == 0 || !request.BirthDate.HasValue;
            if (!succes)
            {
                failure = new DomainFailure("birth date is mandatory.",
                    nameof(request.BirthDate),
                    typeof(BirthDateIsMandatoryException));
            }
            return await Task.FromResult(new RuleResult(succes, failure)).ConfigureAwait(false);
        }
    }
}