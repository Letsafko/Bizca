namespace Bizca.Bff.Application.UseCases.RegisterSmsCodeConfirmation
{
    using Bizca.Bff.Domain.Entities.User;
    using Bizca.Bff.Domain.Entities.User.Exceptions;
    using Bizca.Bff.Domain.Wrappers.Users;
    using Bizca.Bff.Domain.Wrappers.Users.Requests;
    using Bizca.Core.Application.Commands;
    using Bizca.Core.Application.Services;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;
    public sealed class RegisterSmsCodeConfirmationUseCase : ICommandHandler<RegisterSmsCodeConfirmationCommand>
    {
        private readonly IRegisterSmsCodeConfirmationOutput confirmationOutput;
        private readonly IUserChannelWrapper userChannelAgent;
        private readonly IUserRepository userRepository;
        private readonly IEventService eventService;
        public RegisterSmsCodeConfirmationUseCase(IUserWrapper userChannelAgent,
            IRegisterSmsCodeConfirmationOutput confirmationOutput,
            IUserRepository userRepository,
            IEventService eventService)
        {
            this.confirmationOutput = confirmationOutput;
            this.userChannelAgent = userChannelAgent;
            this.userRepository = userRepository;
            this.eventService = eventService;
        }

        public async Task<Unit> Handle(RegisterSmsCodeConfirmationCommand command, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByPhoneNumberAsync(command.PhoneNumber);
            if (user is null || !IsEqualIdentifier(user, command.ExternalUserId))
            {
                throw new UserDoesNotExistException($"user {command.ExternalUserId} with {command.PhoneNumber} does not exist.");
            }

            var CodeConfirmationRequest = new RegisterUserConfirmationCodeRequest(command.ExternalUserId,
                command.ChannelType);

            var CodeConfirmationResponse = await userChannelAgent.RegisterChannelConfirmationCodeAsync(CodeConfirmationRequest);
            if (!CodeConfirmationResponse.Success)
            {
                confirmationOutput.Invalid(CodeConfirmationResponse);
                return Unit.Value;
            }

            user.RegisterSendSmsEvent(command.PartnerCode,
                command.PhoneNumber,
                CodeConfirmationResponse.Data.ConfirmationCode);

            eventService.Enqueue(user.UserEvents);
            confirmationOutput.Ok();
            return Unit.Value;
        }

        private bool IsEqualIdentifier(User user, string externalUserId)
        {
            return user.UserIdentifier.ExternalUserId.Equals(externalUserId,
                System.StringComparison.OrdinalIgnoreCase);
        }
    }
}
