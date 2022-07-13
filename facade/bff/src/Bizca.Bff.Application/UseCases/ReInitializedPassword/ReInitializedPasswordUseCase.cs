namespace Bizca.Bff.Application.UseCases.ReInitializedPassword
{
    using Bizca.Bff.Domain;
    using Bizca.Bff.Domain.Entities.User;
    using Bizca.Bff.Domain.Entities.User.Exceptions;
    using Bizca.Bff.Domain.Wrappers.Notification.Requests.Email;
    using Bizca.Core.Application.Commands;
    using Bizca.Core.Application.Services;
    using Bizca.Core.Domain.EmailTemplate;
    using Bizca.Core.Domain.Services;
    using MediatR;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    public sealed class ReInitializedPasswordUseCase : ICommandHandler<ReInitializedPasswordCommand>
    {
        private readonly IReInitializedPasswordOutput output;
        private readonly IReferentialService referentialService;
        private readonly IUserRepository userRepository;
        private readonly IEventService eventService;
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
            var user = await userRepository.GetByEmailAsync(command.Email);
            if (user is null)
            {
                throw new UserDoesNotExistException($"no user found for email {command.Email}.");
            }

            var emailTemplate = await referentialService.GetEmailTemplateByIdAsync((int)EmailTemplateType.PasswordReset, true);
            var recipients = new List<MailAddressRequest>
            {
                new MailAddressRequest(command.Email)
            };

            var parameters = GetParameters(user);
            user.RegisterSendTransactionalEmailEvent(recipients: recipients,
                emailTemplate: emailTemplate.EmailTemplateId,
                parameters: parameters);

            eventService.Enqueue(user.UserEvents);
            output.Ok(new ReInitializedPasswordDto(true));
            return Unit.Value;
        }

        private static IDictionary<string, object> GetParameters(User user)
        {
            var token = $"{user.UserProfile.Email}:{user.UserIdentifier.ExternalUserId}";
            var tokenBytes = Encoding.UTF8.GetBytes(token);
            var tokenBase64 = Convert.ToBase64String(tokenBytes);

            var reInitUserPasswordUrl = $"https://integ-bizca-front.azurewebsites.net/init-password/{tokenBase64}";
            return new Dictionary<string, object>
            {
                [AttributeConstant.Parameter.ReInitUserPasswordUrl] = reInitUserPasswordUrl
            };
        }
    }
}
