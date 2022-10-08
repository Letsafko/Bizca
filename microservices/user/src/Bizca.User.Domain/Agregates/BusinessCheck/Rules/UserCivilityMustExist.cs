namespace Bizca.User.Domain.Agregates.BusinessCheck.Rules
{
    using Core.Domain;
    using Core.Domain.Exceptions;
    using Core.Domain.Referential.Exception;
    using Core.Domain.Referential.Repository;
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
            bool succes = await civilityRepository.GetByIdAsync(request.Civility ?? 0).ConfigureAwait(false) != null;
            if (!succes)
                failure = new DomainFailure($"civility::{request.Civility} does not exist.",
                    nameof(request.Civility),
                    typeof(CivilityDoesNotExistException));
            return new RuleResult(succes, failure);
        }
    }
}