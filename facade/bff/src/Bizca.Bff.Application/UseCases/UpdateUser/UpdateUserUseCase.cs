namespace Bizca.Bff.Application.UseCases.UpdateUser
{
    using Bizca.Bff.Domain.Entities.User;
    using Bizca.Bff.Domain.Entities.User.Events;
    using Bizca.Bff.Domain.Entities.User.Exceptions;
    using Bizca.Bff.Domain.Enumerations;
    using Bizca.Bff.Domain.Wrappers.Users;
    using Bizca.Bff.Domain.Wrappers.Users.Requests;
    using Bizca.Bff.Domain.Wrappers.Users.Responses;
    using Bizca.Core.Application.Commands;
    using Bizca.Core.Application.Services;
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class UpdateUserUseCase : ICommandHandler<UpdateUserCommand>
    {
        private readonly IUserProfileWrapper userProfileAgent;
        private readonly IUpdateUserOutput updateUserOutput;
        private readonly IUserRepository userRepository;
        private readonly IEventService eventService;
        public UpdateUserUseCase(IUpdateUserOutput updateUserOutput,
            IUserRepository userRepository,
            IEventService eventService,
            IUserWrapper userAgent)
        {
            this.updateUserOutput = updateUserOutput;
            this.userRepository = userRepository;
            this.eventService = eventService;
            this.userProfileAgent = userAgent;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            User user = await userRepository.GetByExternalUserIdAsync(request.ExternalUserId);
            if (user is null)
            {
                throw new UserDoesNotExistException($"user {request.ExternalUserId} does not exist.");
            }

            user.RegisterUserUpdatedEvent(new UserUpdatedNotification(request.ExternalUserId));
            var civility = !string.IsNullOrWhiteSpace(request.Civility)
                    ? Enum.Parse<Civility>(request.Civility)
                    : default(Civility?);

            user.UpdateUserProfile(civility,
                request.FirstName,
                request.LastName,
                request.PhoneNumber,
                request.Whatsapp,
                request.Email);

            await userRepository.UpdateAsync(user);
            var userToUpdateRequest = GetUserRequest(request);
            var response = await userProfileAgent.UpdateUserAsync(request.ExternalUserId, userToUpdateRequest);

            eventService.Enqueue(user.UserEvents);
            var updateUserDto = MapTo(user.Role, response);
            updateUserOutput.Ok(updateUserDto);
            return Unit.Value;
        }

        #region private helpers

        private UserToUpdateRequest GetUserRequest(UpdateUserCommand request)
        {
            return new UserToUpdateRequest(request.FirstName,
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