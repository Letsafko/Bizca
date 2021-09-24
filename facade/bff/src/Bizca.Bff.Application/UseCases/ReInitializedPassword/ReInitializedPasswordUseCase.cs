namespace Bizca.Bff.Application.UseCases.ReInitializedPassword
{
    using Bizca.Bff.Application.Properties;
    using Bizca.Bff.Domain.Entities.User;
    using Bizca.Bff.Domain.Entities.User.Exceptions;
    using Bizca.Bff.Domain.Wrappers.Notification.Requests.Email;
    using Bizca.Core.Application.Commands;
    using Bizca.Core.Application.Services;
    using MediatR;
    using System;
    using System.Collections.Generic;
    using System.Text;
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
            var sender = new MailAddressRequest(command.PartnerCode, Resources.BIZCA_NO_REPLY_EMAIL);
            var recipients = new List<MailAddressRequest>
            {
                new MailAddressRequest(fullName, command.Email)
            };

            var httpContent = GetHtmlContent(user.UserIdentifier.ExternalUserId, command.Email);
            user.RegisterSendEmailEvent(sender,
                recipients,
                Resources.EMAIL_REINIT_PASSWORD_SUBJECT,
                httpContent);

            eventService.Enqueue(user.UserEvents);
            output.Ok(new ReInitializedPasswordDto(true));
            return Unit.Value;
        }

        private string GetHtmlContent(string externalUserId, string email)
        {
            string concatStr = $"{email}:{externalUserId}";
            byte[] bytes = Encoding.UTF8.GetBytes(concatStr);
            string base64Str = Convert.ToBase64String(bytes);
            return $"<p><span style='color: #ffffff; font-weight: normal; vertical-align: middle; background-color: #0092ff; " +
                   $"border-radius: 15px; border: 0px None #000; padding: 8px 20px 8px 20px;'> <a style='text-decoration: none; " +
                   $"color: #ffffff; font-weight: normal;' target='_blank' rel='noreferrer'" +
                   $"href='https://integ-bizca-front.azurewebsites.net/#/init-password/{base64Str}'>Réinitialiser votre mot de passe</a></span></p>";
        }
    }
}
