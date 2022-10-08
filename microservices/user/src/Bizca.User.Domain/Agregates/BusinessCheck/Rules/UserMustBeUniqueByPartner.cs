namespace Bizca.User.Domain.Agregates.BusinessCheck.Rules
{
    using Core.Domain;
    using Core.Domain.Exceptions;
    using Exceptions;
    using Repositories;
    using System.Threading.Tasks;

    public sealed class UserMustBeUniqueByPartner : IUserRule
    {
        private readonly IUserRepository userRepository;

        public UserMustBeUniqueByPartner(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<RuleResult> CheckAsync(UserRequest request)
        {
            DomainFailure failure = null;
            bool succes = !await userRepository.IsExistAsync(request.Partner.Id, request.ExternalUserId)
                .ConfigureAwait(false);
            if (!succes)
                failure = new DomainFailure(
                    $"user::{request.ExternalUserId} for partner::{request.Partner.PartnerCode} must be unique.",
                    nameof(request.ExternalUserId),
                    typeof(UserAlreadyExistException));
            return new RuleResult(succes, failure);
        }
    }
}