namespace Bizca.Bff.Application.UseCases.CreateNewUser
{
    using Bizca.Bff.Domain.Entities.User;
    using Bizca.Bff.Domain.Entities.User.Events;
    using Bizca.Bff.Domain.Entities.User.Factories;
    using Bizca.Bff.Domain.Enumerations;
    using Bizca.Bff.Domain.Wrappers.Users;
    using Bizca.Bff.Domain.Wrappers.Users.Requests;
    using Bizca.Bff.Domain.Wrappers.Users.Responses;
    using Bizca.Core.Application.Commands;
    using Bizca.Core.Application.Services;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class CreateUserUseCase : ICommandHandler<CreateUserCommand>
    {
        private readonly ICreateNewUserOutput createUserOutput;
        private readonly IUserRepository userRepository;
        private readonly IEventService eventService;
        private readonly IUserFactory userFactory;
        private readonly IUserWrapper userAgent;
        public CreateUserUseCase(IUserFactory userFactory,
            ICreateNewUserOutput createUserOutput,
            IUserRepository userRepository,
            IEventService eventService,
            IUserWrapper userAgent)
        {
            this.createUserOutput = createUserOutput;
            this.userRepository = userRepository;
            this.eventService = eventService;
            this.userFactory = userFactory;
            this.userAgent = userAgent;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            UserRequest userRequest = GetUserRequest(request);
            User user = userFactory.Create(userRequest);
            await userRepository.AddAsync(user);
            user.RegisterUserCreatedEvent(new UserCreatedNotification(request.ExternalUserId));

            UserToCreateRequest userToCreateRequest = MapTo(user);
            UserCreatedResponse response = await userAgent.CreateUserAsync(userToCreateRequest);
            CreateNewUserDto newUserDto = MapTo(request.Role, response);

            eventService.Enqueue(user.UserEvents);
            createUserOutput.Ok(newUserDto);
            return Unit.Value;
        }

        #region private helpers

        private CreateNewUserDto MapTo(Role role, UserCreatedResponse response)
        {
            return new CreateNewUserDto(response.ExternalUserId,
                response.FirstName,
                response.LastName,
                response.Civility,
                role,
                response.Channels);
        }

        private UserRequest GetUserRequest(CreateUserCommand request)
        {
            return new UserRequest(request.ExternalUserId,
            request.PartnerCode,
            request.Civility,
            request.EconomicActivity,
            request.PhoneNumber,
            request.FirstName,
            request.LastName,
            request.Whatsapp,
            request.Email,
            request.Role);
        }
        private UserToCreateRequest MapTo(User user)
        {
            return new UserToCreateRequest(user.UserIdentifier.ExternalUserId,
                user.UserProfile.FirstName,
                user.UserProfile.LastName,
                (int)user.UserProfile.Civility,
                user.UserProfile.PhoneNumber,
                user.UserProfile.Whatsapp,
                user.UserProfile.Email);
        }

        #endregion
    }
}