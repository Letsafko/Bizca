namespace Bizca.Bff.Application.UseCases.AuthenticateUser
{
    using Core.Application.Queries;
    using Core.Domain;
    using Domain.Entities.User;
    using Domain.Entities.User.Exceptions;
    using Domain.Enumerations;
    using Domain.Wrappers.Users;
    using Domain.Wrappers.Users.Requests;
    using Domain.Wrappers.Users.Responses;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class AuthenticateUserUseCase : IQueryHandler<AuthenticateUserQuery>
    {
        private readonly IAuthenticateUserOutput authenticateUserOutput;
        private readonly IUserAuthenticationWrapper userAuthenticationAgent;
        private readonly IUserRepository userRepository;

        public AuthenticateUserUseCase(IUserWrapper userAgent,
            IUserRepository userRepository,
            IAuthenticateUserOutput authenticateUserOutput)
        {
            this.authenticateUserOutput = authenticateUserOutput;
            userAuthenticationAgent = userAgent;
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
            IPublicResponse<AuthenticateUserResponse> response =
                await userAuthenticationAgent.AuthenticateUserAsync(request);
            if (!response.Success)
            {
                authenticateUserOutput.Invalid(response);
                return Unit.Value;
            }


            User user = await userRepository.GetByExternalUserIdAsync(response.Data.ExternalUserId);
            if (user is null)
                throw new UserDoesNotExistException($"user {response.Data.ExternalUserId} does not exist.");

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