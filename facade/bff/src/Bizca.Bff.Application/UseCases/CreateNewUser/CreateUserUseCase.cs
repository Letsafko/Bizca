namespace Bizca.Bff.Application.UseCases.CreateNewUser
{
    using Bizca.Bff.Domain.Entities.User;
    using Bizca.Bff.Domain.Entities.User.Factories;
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
        private readonly IUserRepository userRepository;
        private readonly IEventService eventService;
        private readonly IUserFactory userFactory;
        private readonly IUserWrapper userAgent;
        public CreateUserUseCase(IUserFactory userFactory,
            IUserRepository userRepository,
            IEventService eventService,
            IUserWrapper userAgent)
        {
            this.userRepository = userRepository;
            this.eventService = eventService;
            this.userFactory = userFactory;
            this.userAgent = userAgent;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            UserRequest userRequest = GetUserRequest(request);
            User user = userFactory.Create(userRequest);
            if (await userRepository.AddAsync(user))
            {
                UserToCreateRequest userToCreateRequest = MapTo(user);
                UserCreatedResponse response = await userAgent.CreateUserAsync(userToCreateRequest);
                eventService.Enqueue(user.UserEvents);
            }
            return Unit.Value;
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
            request.Email);
        }
        private UserToCreateRequest MapTo(User user)
        {
            return new UserToCreateRequest(user.UserIdentifier.ExternalUserId,
                user.UserIdentifier.PartnerCode,
                user.UserProfile.FirstName,
                user.UserProfile.LastName,
                user.UserProfile.Civility.ToString(),
                user.UserProfile.PhoneNumber,
                user.UserProfile.Whatsapp,
                user.UserProfile.Email);
        }
    }
}
