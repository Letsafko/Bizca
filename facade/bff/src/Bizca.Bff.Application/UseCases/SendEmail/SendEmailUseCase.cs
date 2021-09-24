namespace Bizca.Bff.Application.UseCases.SendEmail
{
    using Bizca.Bff.Domain.Entities.User.Events;
    using Bizca.Bff.Domain.Wrappers.Notification;
    using Bizca.Bff.Domain.Wrappers.Notification.Requests.Email;
    using Bizca.Core.Application.Events;
    using Bizca.Core.Domain.Exceptions;
    using System.Threading;
    using System.Threading.Tasks;
    public sealed class SendEmailUseCase : IEventHandler<SendEmailNotification>
    {
        private readonly INotificationWrapper notificationAgent;
        public SendEmailUseCase(INotificationWrapper notificationAgent)
        {
            this.notificationAgent = notificationAgent;
        }

        public async Task Handle(SendEmailNotification notification, CancellationToken cancellationToken)
        {
            var request = new TransactionalEmailRequest(sender: notification.Sender,
                to: notification.To,
                subject: notification.Subject,
                htmlContent: notification.HtmlContent);

            var response = await notificationAgent.SendEmail(request);
            if (!response.Success)
                throw new DomainException(response.Message.ToString());
        }
    }
}
