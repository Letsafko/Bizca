namespace Bizca.Bff.Application.UseCases.UpdateUser
{
    using Core.Application.Commands;
    using Core.Application.Services;
    using Core.Domain;
    using Domain.Entities.User;
    using Domain.Entities.User.Exceptions;
    using Domain.Enumerations;
    using Domain.Wrappers.Users;
    using Domain.Wrappers.Users.Requests;
    using Domain.Wrappers.Users.Responses;
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class UpdateUserUseCase : ICommandHandler<UpdateUserCommand>
    {
        private readonly IEventService eventService;
        private readonly IUpdateUserOutput updateUserOutput;
        private readonly IUserProfileWrapper userProfileAgent;
        private readonly IUserRepository userRepository;

        public UpdateUserUseCase(IUpdateUserOutput updateUserOutput,
            IUserRepository userRepository,
            IEventService eventService,
            IUserWrapper userAgent)
        {
            this.updateUserOutput = updateUserOutput;
            this.userRepository = userRepository;
            this.eventService = eventService;
            userProfileAgent = userAgent;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            User user = await userRepository.GetByExternalUserIdAsync(request.ExternalUserId);
            if (user is null) throw new UserDoesNotExistException($"user {request.ExternalUserId} does not exist.");

            user.RegisterUserUpdatedEvent(request.ExternalUserId);
            Civility? civility = !string.IsNullOrWhiteSpace(request.Civility)
                ? Enum.Parse<Civility>(request.Civility)
                : default(Civility?);

            user.UpdateUserProfile(civility,
                request.FirstName,
                request.LastName,
                request.PhoneNumber,
                request.Whatsapp,
                request.Email);

            await userRepository.UpdateAsync(user);
            UserToUpdateRequest userToUpdateRequest = GetUserRequest(request);
            IPublicResponse<UserUpdatedResponse> response = await userProfileAgent.UpdateUserAsync(userToUpdateRequest);
            if (!response.Success)
            {
                updateUserOutput.Invalid(response);
                return Unit.Value;
            }

            eventService.Enqueue(user.UserEvents);
            UpdateUserDto updateUserDto = MapTo(user.Role, response.Data);
            updateUserOutput.Ok(updateUserDto);
            return Unit.Value;
        }

        #region private helpers

        private UserToUpdateRequest GetUserRequest(UpdateUserCommand request)
        {
            return new UserToUpdateRequest(request.ExternalUserId,
                request.FirstName,
                request.LastName,
                request.Civility,
                request.PhoneNumber,
                request.Whatsapp,
                request.Email);
        }

        private UpdateUserDto MapTo(Role role, UserUpdatedResponse response)
        {
            return new UpdateUserDto(response.ExternalUserId,
                response.FirstName,
                response.LastName,
                response.Civility,
                role,
                response.Channels);
        }

        #endregion
    }
}