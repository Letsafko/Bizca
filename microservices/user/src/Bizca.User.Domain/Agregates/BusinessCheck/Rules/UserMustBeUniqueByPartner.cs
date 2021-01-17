namespace Bizca.User.Domain.Agregates.BusinessCheck.Rules
{
    using Bizca.Core.Domain;
    using Bizca.Core.Domain.Exceptions;
    using Bizca.User.Domain.Agregates.BusinessCheck.Exceptions;
    using Bizca.User.Domain.Agregates.Repositories;
    using System.Threading.Tasks;

    public sealed class UserMustBeUniqueByPartner : IUserRule
    {
        private readonly IUserRepository _userRepository;
        public UserMustBeUniqueByPartner(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<RuleResult> CheckAsync(UserRequest request)
        {
            DomainFailure failure = null;
            bool succes = !await _userRepository.IsExistAsync(request.Partner.Id, request.ExternalUserId).ConfigureAwait(false);
            if (!succes)
            {
                failure = new DomainFailure($"user::{request.ExternalUserId} for partner::{request.Partner.PartnerCode} must be unique.",
                    nameof(request.ExternalUserId),
                    typeof(UserAlreadyExistException));
            }
            return new RuleResult(succes, failure);
        }
    }
}