namespace Bizca.Bff.Application.UseCases.GetUserDetails
{
    using Core.Domain;
    using Core.Domain.Cqrs.Queries;
    using Domain.Entities.User;
    using Domain.Entities.User.Exceptions;
    using Domain.Enumerations;
    using Domain.Wrappers.Users;
    using Domain.Wrappers.Users.Responses;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class GetUserDetailsUseCase : IQueryHandler<GetUserDetailsQuery>
    {
        private readonly IGetUserDetailsOutput output;
        private readonly IUserProfileWrapper userProfileWrapper;
        private readonly IUserRepository userRepository;

        public GetUserDetailsUseCase(IUserRepository userRepository,
            IUserWrapper userWrapper,
            IGetUserDetailsOutput output)
        {
            userProfileWrapper = userWrapper;
            this.userRepository = userRepository;
            this.output = output;
        }

        public async Task<Unit> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
        {
            (IPublicResponse<UserResponse> response, User user) =
                await GetEntitiesAsync(request.PartnerCode, request.ExternalUserId);
            if (user is null) throw new UserDoesNotExistException($"user {request.ExternalUserId} does not exist.");

            if (!response.Success)
            {
                output.Invalid(response);
                return Unit.Value;
            }

            GetUserDetailsDto getUserDetailsdto = MapTo(user.Role, response.Data);
            output.Ok(getUserDetailsdto);
            return Unit.Value;
        }

        #region private helpers

        private async Task<(IPublicResponse<UserResponse> response, User user)> GetEntitiesAsync(string partnerCode,
            string externalUserId)
        {
            Task<IPublicResponse<UserResponse>> userWrapperTask =
                userProfileWrapper.GetUserDetailsAsync(partnerCode, externalUserId);
            Task<User> userLocalTask = userRepository.GetByExternalUserIdAsync(externalUserId);
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