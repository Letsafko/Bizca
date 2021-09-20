namespace Bizca.Bff.Application.UseCases.SendConfirmationEmail
{
    using Bizca.Bff.Application.Properties;
    using Bizca.Bff.Domain.Entities.User.Events;
    using Bizca.Bff.Domain.Wrappers.Notification;
    using Bizca.Bff.Domain.Wrappers.Notification.Requests.Email;
    using Bizca.Core.Application.Events;
    using Bizca.Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class SendConfirmationEmailUseCase : IEventHandler<SendConfirmationEmailNotification>
    {
        private readonly INotificationWrapper notificationAgent;
        public SendConfirmationEmailUseCase(INotificationWrapper notificationAgent)
        {
            this.notificationAgent = notificationAgent;
        }

        public async Task Handle(SendConfirmationEmailNotification notification, CancellationToken cancellationToken)
        {
            string htmlContent = GetHtmlContent(notification.ExternalUserId,
                   notification.CodeConfirmation,
                   notification.Email);

            var sender = new MailAddressRequest(notification.PartnerCode, Resources.BIZCA_NO_REPLY_EMAIL);
            var recipient = new MailAddressRequest(notification.FullName, notification.Email);

            var request = new TransactionalEmailRequest(sender: sender,
                to: new List<MailAddressRequest> { recipient },
                subject: Resources.EMAIL_CONFIRMATION_SUBJECT,
                htmlContent: htmlContent);

            var response = await notificationAgent.SendEmail(request);
            if (!response.Success)
                throw new DomainException(response.Message.ToString());
        }

        private string GetHtmlContent(string externalUserId, string codeConfirmation, string resource)
        {
            string concatStr = $"{resource}:{externalUserId}:{codeConfirmation}";
            byte[] bytes = Encoding.UTF8.GetBytes(concatStr);
            string base64Str = Convert.ToBase64String(bytes);
            return $"<p><span style='color: #ffffff; font-weight: normal; vertical-align: middle; background-color: #0092ff; " +
                   $"border-radius: 15px; border: 0px None #000; padding: 8px 20px 8px 20px;'> <a style='text-decoration: none; " +
                   $"color: #ffffff; font-weight: normal;' target='_blank' rel='noreferrer'" +
                   $"href='https://integ-bizca-front.azurewebsites.net/#/create-password/{base64Str}'>Confirmer votre adresse email</a></span></p>";
        }
    }
}
