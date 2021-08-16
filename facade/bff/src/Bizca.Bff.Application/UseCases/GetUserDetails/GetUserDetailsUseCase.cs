namespace Bizca.Bff.Application.UseCases.GetUserDetails
{
    using Bizca.Bff.Domain.Entities.User;
    using Bizca.Bff.Domain.Entities.User.Exceptions;
    using Bizca.Bff.Domain.Enumerations;
    using Bizca.Bff.Domain.Wrappers.Users;
    using Bizca.Bff.Domain.Wrappers.Users.Responses;
    using Bizca.Core.Application.Queries;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class GetUserDetailsUseCase : IQueryHandler<GetUserDetailsQuery>
    {
        private readonly IUserRepository userRepository;
        private readonly IGetUserDetailsOutput output;
        private readonly IUserWrapper userWrapper;
        public GetUserDetailsUseCase(IUserRepository userRepository,
            IUserWrapper userWrapper,
            IGetUserDetailsOutput output)
        {
            this.userRepository = userRepository;
            this.userWrapper = userWrapper;
            this.output = output;
        }

        public async Task<Unit> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
        {
            (UserResponse response, User user) = await GetEntitiesAsync(request.PartnerCode, request.ExternalUserId);
            if (user is null)
            {
                throw new UserDoesNotExistException($"user {request.ExternalUserId} does not exist.");
            }

            var getUserDetailsdto = MapTo(user.Role, response);
            output.Ok(getUserDetailsdto);
            return Unit.Value;
        }

        #region private helpers

        private async Task<(UserResponse response, User user)> GetEntitiesAsync(string partnerCode, string externalUserId)
        {
            var userWrapperTask = userWrapper.GetUserDetailsAsync(partnerCode, externalUserId);
            var userLocalTask = userRepository.GetByExternalUserIdAsync(externalUserId);
            await Task.WhenAll(userLocalTask, userWrapperTask);
            return
            (
                userWrapperTask.Result,
                userLocalTask.Result
            );
        }
        private GetUserDetailsDto MapTo(Role role, UserResponse response)
        {
            return new GetUserDetailsDto(response.ExternalUserId,
                response.FirstName,
                response.LastName,
                response.Civility,
                role,
                response.Channels);
        }

        #endregion
    }
}
