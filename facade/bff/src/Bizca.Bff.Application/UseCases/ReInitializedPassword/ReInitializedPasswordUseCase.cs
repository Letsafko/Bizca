namespace Bizca.Bff.Application.UseCases.ReInitializedPassword
{
    using Core.Application.Commands;
    using Core.Application.Services;
    using Core.Domain.Referential.Model;
    using Core.Domain.Referential.Services;
    using Domain;
    using Domain.Entities.User;
    using Domain.Entities.User.Exceptions;
    using Domain.Wrappers.Notification.Requests.Email;
    using MediatR;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class ReInitializedPasswordUseCase : ICommandHandler<ReInitializedPasswordCommand>
    {
        private readonly IEventService eventService;
        private readonly IReInitializedPasswordOutput output;
        private readonly IReferentialService referentialService;
        private readonly IUserRepository userRepository;

        public ReInitializedPasswordUseCase(IUserRepository userRepository,
            IReferentialService referentialService,
            IEventService eventService,
            IReInitializedPasswordOutput output)
        {
            this.referentialService = referentialService;
            this.userRepository = userRepository;
            this.eventService = eventService;
            this.output = output;
        }

        public async Task<Unit> Handle(ReInitializedPasswordCommand command, CancellationToken cancellationToken)
        {
            User user = await userRepository.GetByEmailAsync(command.Email);
            if (user is null) throw new UserDoesNotExistException($"no user found for email {command.Email}.");

            EmailTemplate emailTemplate =
                await referentialService.GetEmailTemplateByIdAsync((int)EmailTemplateType.PasswordReset, true);
            var recipients = new List<MailAddressRequest> { new MailAddressRequest(command.Email) };

            IDictionary<string, object> parameters = GetParameters(user);
            user.RegisterSendTransactionalEmailEvent(recipients,
                emailTemplate: emailTemplate.EmailTemplateId,
                parameters: parameters);

            eventService.Enqueue(user.UserEvents);
            output.Ok(new ReInitializedPasswordDto(true));
            return Unit.Value;
        }

        private static IDictionary<string, object> GetParameters(User user)
        {
            string token = $"{user.UserProfile.Email}:{user.UserIdentifier.ExternalUserId}";
            byte[] tokenBytes = Encoding.UTF8.GetBytes(token);
            string tokenBase64 = Convert.ToBase64String(tokenBytes);

            string reInitUserPasswordUrl = $"https://integ-bizca-front.azurewebsites.net/init-password/{tokenBase64}";
            return new Dictionary<string, object>
            {
                [AttributeConstant.Parameter.ReInitUserPasswordUrl] = reInitUserPasswordUrl
            };
        }
    }
}