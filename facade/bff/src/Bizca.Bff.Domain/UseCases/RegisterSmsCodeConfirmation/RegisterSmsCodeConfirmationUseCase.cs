namespace Bizca.Bff.Application.UseCases.RegisterSmsCodeConfirmation
{
    using Core.Domain;
    using Core.Domain.Cqrs.Commands;
    using Core.Domain.Cqrs.Services;
    using Domain.Entities.User;
    using Domain.Entities.User.Exceptions;
    using Domain.Wrappers.Users;
    using Domain.Wrappers.Users.Requests;
    using Domain.Wrappers.Users.Responses;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class RegisterSmsCodeConfirmationUseCase : ICommandHandler<RegisterSmsCodeConfirmationCommand>
    {
        private readonly IRegisterSmsCodeConfirmationOutput confirmationOutput;
        private readonly IEventService eventService;
        private readonly IUserChannelWrapper userChannelAgent;
        private readonly IUserRepository userRepository;

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
            User user = await userRepository.GetByPhoneNumberAsync(command.PhoneNumber);
            if (user is null)
                throw new UserDoesNotExistException(
                    $"user {command.ExternalUserId} with {command.PhoneNumber} does not exist.");

            var CodeConfirmationRequest = new RegisterUserConfirmationCodeRequest(command.ExternalUserId,
                command.ChannelType);

            IPublicResponse<RegisterUserConfirmationCodeResponse> CodeConfirmationResponse =
                await userChannelAgent.RegisterChannelConfirmationCodeAsync(CodeConfirmationRequest);
            if (!CodeConfirmationResponse.Success)
            {
                confirmationOutput.Invalid(CodeConfirmationResponse);
                return Unit.Value;
            }

            user.RegisterSendTransactionalSmsEvent(command.PartnerCode,
                command.PhoneNumber,
                CodeConfirmationResponse.Data.ConfirmationCode);

            eventService.Enqueue(user.UserEvents);
            confirmationOutput.Ok();
            return Unit.Value;
        }
    }
}