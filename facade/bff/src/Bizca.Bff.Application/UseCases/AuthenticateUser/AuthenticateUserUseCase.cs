namespace Bizca.Bff.Application.UseCases.AuthenticateUser
{
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
        private readonly IUserWrapper userAgent;
        public AuthenticateUserUseCase(IUserWrapper userAgent,
            IAuthenticateUserOutput authenticateUserOutput)
        {
            this.authenticateUserOutput = authenticateUserOutput;
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

            AuthenticateUserDto authenticateUser = MapTo(response);
            authenticateUserOutput.Ok(authenticateUser);
            return Unit.Value;
        }

        private AuthenticateUserDto MapTo(AuthenticateUserResponse response)
        {
            return new AuthenticateUserDto(response.ExternalUserId,
                response.FirstName,
                response.LastName,
                response.Civility,
                response.Channels);
        }
    }
}