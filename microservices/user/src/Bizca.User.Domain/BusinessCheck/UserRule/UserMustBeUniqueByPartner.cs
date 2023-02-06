namespace Bizca.User.Domain.BusinessCheck.UserRule
{
    using Agregates;
    using Agregates.Repositories;
    using Contract;
    using Core.Domain.Rules;
    using System.Threading.Tasks;

    public sealed class UserMustBeUniqueByPartner : IUserRule
    {
        private readonly IUserRepository _userRepository;

        public UserMustBeUniqueByPartner(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<CheckResult> CheckAsync(UserRequest request)
        {
            bool success = !await _userRepository.IsExistAsync(request.Partner.PartnerId, request.ExternalUserId);

            CheckReport checkReport = null;
            if (!success)
                checkReport = new CheckReport(
                    $"user::{request.ExternalUserId} for partner::{request.Partner.PartnerCode} must be unique.",
                    nameof(request.ExternalUserId));

            return new CheckResult(success, checkReport);
        }
    }
}