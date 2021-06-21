namespace Bizca.Bff.Application.UseCases.SendConfirmationEmail
{
    using Bizca.Bff.Domain.Entities.User.Events;
    using Bizca.Bff.Domain.Enumerations;
    using Bizca.Bff.Domain.Wrappers.Notification;
    using Bizca.Bff.Domain.Wrappers.Notification.Requests;
    using Bizca.Bff.Domain.Wrappers.Users;
    using Bizca.Bff.Domain.Wrappers.Users.Requests;
    using Bizca.Bff.Domain.Wrappers.Users.Responses;
    using Bizca.Core.Application.Events;
    using System;
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

        public async Task Handle(SendConfirmationEmalNotification notification, CancellationToken cancellationToken)
        {
            var CodeConfirmationRequest = new RegisterUserConfirmationCodeRequest(ChannelType.Email);
            UserConfirmationCodeResponse CodeConfirmationResponse = await userAgent.RegisterChannelConfirmationCodeAsync(notification.ExternalUserId,
                CodeConfirmationRequest);

            string callbackUrl = GetCallbackUrl(notification.ExternalUserId, CodeConfirmationResponse);
            var request = new TransactionalEmailRequest(from: "no-reply@bizca.fr",
                to: notification.Email,
                subject: "Confirmation d'email",
                body: callbackUrl);

            await notificationAgent.SendConfirmationEmail(request);
        }

        private string GetCallbackUrl(string externalUserId, UserConfirmationCodeResponse response)
        {
            string concatStr = $"{response.Resource}:{externalUserId}:{response.ConfirmationCode}";
            byte[] bytes = Encoding.UTF8.GetBytes(concatStr);
            string base64Str = Convert.ToBase64String(bytes);
            return $"http://localhost:4200/#/create-password/{base64Str}";
        }
    }
}
