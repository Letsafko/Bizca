namespace Bizca.Bff.Application.UseCases.SendSms
{
    using Bizca.Bff.Domain.Events;
    using Bizca.Bff.Domain.Wrappers.Notification;
    using Bizca.Bff.Domain.Wrappers.Notification.Requests.Sms;
    using Bizca.Core.Application.Events;
    using Bizca.Core.Domain.Exceptions;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class SendTransactionalSmsUseCase : IEventHandler<SendTransactionalSmsEvent>
    {
        private readonly INotificationWrapper notificationAgent;
        public SendTransactionalSmsUseCase(INotificationWrapper notificationAgent)
        {
            this.notificationAgent = notificationAgent;
        }

        public async Task Handle(SendTransactionalSmsEvent notification, CancellationToken cancellationToken)
        {
            var request = new TransactionalSmsRequest(sender: notification.Sender,
                recipientPhoneNumber: notification.Recipient,
                content: notification.Content);

            var response = await notificationAgent.SendTransactionalSms(request);
            if (!response.Success)
                throw new DomainException(response.Message.ToString());
        }
    }
}