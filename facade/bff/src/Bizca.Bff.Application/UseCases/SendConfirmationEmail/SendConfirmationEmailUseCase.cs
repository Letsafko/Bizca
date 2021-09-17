namespace Bizca.Bff.Application.UseCases.SendConfirmationEmail
{
    using Bizca.Bff.Application.Properties;
    using Bizca.Bff.Domain.Entities.User.Events;
    using Bizca.Bff.Domain.Wrappers.Notification;
    using Bizca.Bff.Domain.Wrappers.Notification.Requests;
    using Bizca.Bff.Domain.Wrappers.Users;
    using Bizca.Bff.Domain.Wrappers.Users.Requests;
    using Bizca.Bff.Domain.Wrappers.Users.Responses;
    using Bizca.Core.Application.Events;
    using Bizca.Core.Domain;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class SendConfirmationEmailUseCase : IEventHandler<SendConfirmationEmailNotification>
    {
        private readonly INotificationWrapper notificationAgent;
        private readonly IUserChannelWrapper userChannelAgent;
        public SendConfirmationEmailUseCase(IUserWrapper userAgent,
            INotificationWrapper notificationAgent)
        {
            this.notificationAgent = notificationAgent;
            this.userChannelAgent = userAgent;
        }

        public async Task Handle(SendConfirmationEmailNotification notification, CancellationToken cancellationToken)
        {
            var CodeConfirmationRequest = new RegisterUserConfirmationCodeRequest(notification.ChannelType);
            IPublicResponse<RegisterUserConfirmationCodeResponse> CodeConfirmationResponse = await userChannelAgent.RegisterChannelConfirmationCodeAsync(notification.ExternalUserId,
                CodeConfirmationRequest);

            string htmlContent = GetHtmlContent(notification.ExternalUserId, CodeConfirmationResponse.Data);
            var sender = new MailAddressRequest(notification.PartnerCode, Resources.BIZCA_NO_REPLY_EMAIL);
            var recipient = new MailAddressRequest(notification.FullName, notification.Email);

            var request = new TransactionalEmailRequest(sender: sender,
                to: new List<MailAddressRequest> { recipient },
                subject: Resources.EMAIL_CONFIRMATION_SUBJECT,
                htmlContent: htmlContent);

            await notificationAgent.SendEmail(request);
        }

        private string GetHtmlContent(string externalUserId, RegisterUserConfirmationCodeResponse response)
        {
            string concatStr = $"{response.Resource}:{externalUserId}:{response.ConfirmationCode}";
            byte[] bytes = Encoding.UTF8.GetBytes(concatStr);
            string base64Str = Convert.ToBase64String(bytes);
            return $"<p><span style='color: #ffffff; font-weight: normal; vertical-align: middle; background-color: #0092ff; " +
                   $"border-radius: 15px; border: 0px None #000; padding: 8px 20px 8px 20px;'> <a style='text-decoration: none; " +
                   $"color: #ffffff; font-weight: normal;' target='_blank' rel='noreferrer'" +
                   $"href='https://integ-bizca-front.azurewebsites.net/#/create-password/{base64Str}'>Confirmer votre adresse email</a></span></p>";
        }
    }
}
