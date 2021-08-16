namespace Bizca.Bff.Application.UseCases.ReInitializedPassword
{
    using Bizca.Bff.Domain.Entities.User;
    using Bizca.Bff.Domain.Entities.User.Exceptions;
    using Bizca.Core.Application.Commands;
    using Bizca.Core.Application.Services;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;
    public sealed class ReInitializedPasswordUseCase : ICommandHandler<ReInitializedPasswordCommand>
    {
        private readonly IReInitializedPasswordOutput output;
        private readonly IUserRepository userRepository;
        private readonly IEventService eventService;
        public ReInitializedPasswordUseCase(IUserRepository userRepository,
            IEventService eventService,
            IReInitializedPasswordOutput output)
        {
            this.userRepository = userRepository;
            this.eventService = eventService;
            this.output = output;
        }

        public async Task<Unit> Handle(ReInitializedPasswordCommand command, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByEmailAsync(command.Email);
            if (user is null)
            {
                throw new UserDoesNotExistException($"no user found for email {command.Email}.");
            }

            string fullName = $"{user.UserProfile.FirstName} {user.UserProfile.LastName}";
            user.RegisterReInitPasswordEvent(user.UserIdentifier.ExternalUserId,
                user.UserProfile.Email,
                fullName);
            output.Ok(new ReInitializedPasswordDto(true));
            eventService.Enqueue(user.UserEvents);
            return Unit.Value;
        }
    }
}
