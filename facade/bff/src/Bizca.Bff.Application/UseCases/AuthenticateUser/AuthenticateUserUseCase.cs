namespace Bizca.Bff.Application.UseCases.AuthenticateUser
{
    using Bizca.Bff.Domain.Entities.User;
    using Bizca.Bff.Domain.Entities.User.Exceptions;
    using Bizca.Bff.Domain.Enumerations;
    using Bizca.Bff.Domain.Wrappers.Users;
    using Bizca.Bff.Domain.Wrappers.Users.Requests;
    using Bizca.Bff.Domain.Wrappers.Users.Responses;
    using Bizca.Core.Application.Queries;
    using Bizca.Core.Domain;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class AuthenticateUserUseCase : IQueryHandler<AuthenticateUserQuery>
    {
        private readonly IUserAuthenticationWrapper userAuthenticationAgent;
        private readonly IAuthenticateUserOutput authenticateUserOutput;
        private readonly IUserRepository userRepository;
        public AuthenticateUserUseCase(IUserWrapper userAgent,
            IUserRepository userRepository,
            IAuthenticateUserOutput authenticateUserOutput)
        {
            this.authenticateUserOutput = authenticateUserOutput;
            this.userAuthenticationAgent = userAgent;
            this.userRepository = userRepository;
        }

        /// <summary>
        ///     Handle authentication of user
        /// </summary>
        /// <param name="query">Authenticate user query</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Unit> Handle(AuthenticateUserQuery query, CancellationToken cancellationToken)
        {
            var request = new AuthenticateUserRequest(query.Password, query.Resource);
            IPublicResponse<AuthenticateUserResponse> response = await userAuthenticationAgent.AuthenticateUserAsync(request);
            if (!response.Success)
            {
                authenticateUserOutput.Invalid(response);
                return Unit.Value;
            }


            var user = await userRepository.GetByExternalUserIdAsync(response.Data.ExternalUserId);
            if (user is null)
            {
                throw new UserDoesNotExistException($"user {response.Data.ExternalUserId} does not exist.");
            }

            AuthenticateUserDto authenticateUser = MapTo(user.Role, response.Data);
            authenticateUserOutput.Ok(authenticateUser);
            return Unit.Value;
        }

        private AuthenticateUserDto MapTo(Role role, AuthenticateUserResponse response)
        {
            return new AuthenticateUserDto(response.ExternalUserId,
                response.FirstName,
                response.LastName,
                response.Civility,
                role,
                response.Channels);
        }
    }
}