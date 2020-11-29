namespace Bizca.User.Domain.Agregates.Users.Rules
{
    using Bizca.Core.Domain;
    using Bizca.User.Domain.Agregates.Users.Exceptions;
    using Bizca.User.Domain.Agregates.Users.Repositories;
    using System;
    using System.Threading.Tasks;

    public sealed class UserMustBeUniqueByPartner : IUserRule
    {
        private readonly IUserRepository _userRepository;
        public UserMustBeUniqueByPartner(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<RuleResult> CheckAsync(UserRequest request)
        {
            bool result = !await _userRepository.IsExistAsync(request.PartnerId, request.ExternalUserId).ConfigureAwait(false);
            return new RuleResult(result,
                result ? default : $"user::{request.ExternalUserId} for partner::{request.PartnerCode} must be unique.",
                result ? default : typeof(UserAlreadyExistException));
        }
    }
}