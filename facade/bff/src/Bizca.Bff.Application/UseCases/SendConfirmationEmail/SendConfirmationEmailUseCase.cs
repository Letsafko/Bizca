namespace Bizca.Bff.Application.UseCases.SendConfirmationEmail
{
    using Bizca.Bff.Application.Properties;
    using Bizca.Bff.Domain.Entities.User.Events;
    using Bizca.Bff.Domain.Enumerations;
    using Bizca.Bff.Domain.Wrappers.Notification;
    using Bizca.Bff.Domain.Wrappers.Notification.Requests;
    using Bizca.Bff.Domain.Wrappers.Users;
    using Bizca.Bff.Domain.Wrappers.Users.Requests;
    using Bizca.Bff.Domain.Wrappers.Users.Responses;
    using Bizca.Core.Application.Events;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class SendConfirmationEmailUseCase : IEventHandler<SendConfirmationEmalNotification>
    {
        private readonly INotificationWrapper notificationAgent;
        private readonly IUserWrapper userAgent;
        public SendConfirmationEmailUseCase(IUserWrapper userAgent,
            INotificationWrapper notificationAgent)
        {
            this.notificationAgent = notificationAgent;
            this.userAgent = userAgent;
        }

        private const string SenderEmail = "no-reply@bizca.fr";
        public async Task Handle(SendConfirmationEmalNotification notification, CancellationToken cancellationToken)
        {
            var CodeConfirmationRequest = new RegisterUserConfirmationCodeRequest(ChannelType.Email);
            UserConfirmationCodeResponse CodeConfirmationResponse = await userAgent.RegisterChannelConfirmationCodeAsync(notification.ExternalUserId,
                CodeConfirmationRequest);

            string htmlContent = GetHtmlContent(notification.ExternalUserId, CodeConfirmationResponse);
            var sender = new MailAddressRequest(notification.PartnerCode, SenderEmail);
            var recipient = new MailAddressRequest(notification.FullName, notification.Email);

            var request = new TransactionalEmailRequest(sender: sender,
                to: new List<MailAddressRequest> { recipient },
                subject: Resources.EMAIL_CONFIRMATION_SUBJECT,
                htmlContent: htmlContent);

            await notificationAgent.SendConfirmationEmail(request);
        }

        private string GetHtmlContent(string externalUserId, UserConfirmationCodeResponse response)
        {
            string concatStr = $"{response.Resource}:{externalUserId}:{response.ConfirmationCode}";
            byte[] bytes = Encoding.UTF8.GetBytes(concatStr);
            string base64Str = Convert.ToBase64String(bytes);
            return $"<p><span style='color: #ffffff; font-weight: normal; vertical-align: middle; background-color: #0092ff; " +
                   $"border-radius: 15px; border: 0px None #000; padding: 8px 20px 8px 20px;'> <a style='text-decoration: none; " +
                   $"color: #ffffff; font-weight: normal;' target='_blank' rel='noreferrer'" +
                   $"href='http://localhost:4200/#/create-password/{base64Str}'>Confirmer votre adresse email</a></span></p>";
        }
    }
}