namespace Bizca.Bff.Application.UseCases.AuthenticateUser
{
    using Bizca.Bff.Domain.Entities.User;
    using Bizca.Bff.Domain.Enumerations;
    using Bizca.Bff.Domain.Wrappers.Users;
    using Bizca.Bff.Domain.Wrappers.Users.Requests;
    using Bizca.Bff.Domain.Wrappers.Users.Responses;
    using Bizca.Core.Application.Queries;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class AuthenticateUserUseCase : IQueryHandler<AuthenticateUserQuery>
    {
        private readonly IAuthenticateUserOutput authenticateUserOutput;
        private readonly IUserRepository userRepository;
        private readonly IUserWrapper userAgent;
        public AuthenticateUserUseCase(IUserWrapper userAgent,
            IUserRepository userRepository,
            IAuthenticateUserOutput authenticateUserOutput)
        {
            this.authenticateUserOutput = authenticateUserOutput;
            this.userRepository = userRepository;
            this.userAgent = userAgent;
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
            AuthenticateUserResponse response = await userAgent.AutehticateUserAsync(request);
            var user = await userRepository.GetByExternalUserIdAsync(response.ExternalUserId);

            AuthenticateUserDto authenticateUser = MapTo(user.Role, response);
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