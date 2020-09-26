namespace Bizca.User.Domain.Rules
{
    using Bizca.User.Domain.Repositories;
    using System;
    using System.Threading.Tasks;

    public sealed class UserMustBeUniqueForPartner : IBusinessRule<UserRequest>
    {
        private readonly IUserRepository _userRepository;
        public UserMustBeUniqueForPartner(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<bool> CheckAsync(UserRequest request)
        {
            return !await _userRepository.IsExistAsync(request.PartnerId, request.AppUserId).ConfigureAwait(false);
        }
    }
}