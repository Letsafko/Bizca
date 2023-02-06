namespace Bizca.User.Domain.BusinessCheck.UserRule
{
    using Agregates;
    using Contract;
    using Core.Domain.Referential.Repository;
    using Core.Domain.Rules;
    using System.Threading.Tasks;

    public sealed class UserCivilityMustExist : IUserRule
    {
        private readonly ICivilityRepository _civilityRepository;

        public UserCivilityMustExist(ICivilityRepository civilityRepository)
        {
            _civilityRepository = civilityRepository;
        }

        public async Task<CheckResult> CheckAsync(UserRequest request)
        {
            bool success = await _civilityRepository.GetByIdAsync(request.Civility.GetValueOrDefault()) is not null;

            CheckReport checkReport = null;
            if (!success)
                checkReport = new CheckReport($"given civility '{request.Civility}' does not exist.",
                    nameof(request.Civility));

            return new CheckResult(success, checkReport);
        }
    }
}