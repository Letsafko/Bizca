namespace Bizca.User.Domain.Agregates.BusinessCheck.Rules
{
    using Bizca.Core.Domain;
    using Bizca.Core.Domain.Civility;
    using Bizca.Core.Domain.Exceptions;
    using System.Threading.Tasks;

    public sealed class UserCivilityMustExist : IUserRule
    {
        private readonly ICivilityRepository civilityRepository;
        public UserCivilityMustExist(ICivilityRepository civilityRepository)
        {
            this.civilityRepository = civilityRepository;
        }

        public async Task<RuleResult> CheckAsync(UserRequest request)
        {
            DomainFailure failure = null;
            bool succes = await civilityRepository.GetByIdAsync(request.Civility).ConfigureAwait(false) != null;
            if (!succes)
            {
                failure = new DomainFailure($"civility::{request.Civility} does not exist.",
                    nameof(request.Civility),
                    typeof(CivilityDoesNotExistException));
            }
            return new RuleResult(succes, failure);
        }
    }
}