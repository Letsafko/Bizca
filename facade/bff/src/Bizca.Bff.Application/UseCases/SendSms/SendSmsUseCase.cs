namespace Bizca.Bff.Application.UseCases.SendSms
{
    using Bizca.Bff.Domain.Entities.User.Events;
    using Bizca.Bff.Domain.Wrappers.Notification;
    using Bizca.Bff.Domain.Wrappers.Notification.Requests.Sms;
    using Bizca.Core.Application.Events;
    using Bizca.Core.Domain.Exceptions;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class SendSmsUseCase : IEventHandler<SendSmsNotification>
    {
        private readonly INotificationWrapper notificationAgent;
        public SendSmsUseCase(INotificationWrapper notificationAgent)
        {
            this.notificationAgent = notificationAgent;
        }

        public async Task Handle(SendSmsNotification notification, CancellationToken cancellationToken)
        {
            var request = new TransactionalSmsRequest(sender: notification.Sender,
                recipientPhoneNumber: notification.Recipient,
                content: notification.Content);

            var response = await notificationAgent.SendSms(request);
            if (!response.Success)
                throw new DomainException(response.Message.ToString());
        }
    }
}