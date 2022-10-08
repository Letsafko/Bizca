namespace Bizca.Bff.Application.UseCases.SendSms
{
    using Core.Application.Events;
    using Core.Domain;
    using Core.Domain.Exceptions;
    using Domain.Events;
    using Domain.Wrappers.Notification;
    using Domain.Wrappers.Notification.Requests.Sms;
    using Domain.Wrappers.Notification.Responses;
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
            var request = new TransactionalSmsRequest(notification.Sender,
                notification.Recipient,
                notification.Content);

            IPublicResponse<TransactionalSmsResponse> response = await notificationAgent.SendTransactionalSms(request);
            if (!response.Success)
                throw new DomainException(response.Message.ToString());
        }
    }
}