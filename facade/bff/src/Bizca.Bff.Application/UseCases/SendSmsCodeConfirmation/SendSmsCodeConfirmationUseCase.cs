namespace Bizca.Bff.Application.UseCases.SendSmsCodeConfirmation
{
    using Bizca.Bff.Domain.Entities.User.Events;
    using Bizca.Bff.Domain.Wrappers.Notification;
    using Bizca.Bff.Domain.Wrappers.Notification.Requests.Sms;
    using Bizca.Core.Application.Events;
    using Bizca.Core.Domain.Exceptions;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class SendSmsCodeConfirmationUseCase : IEventHandler<SendSmsCodeConfirmationNotification>
    {
        private readonly INotificationWrapper notificationAgent;
        public SendSmsCodeConfirmationUseCase(INotificationWrapper notificationAgent)
        {
            this.notificationAgent = notificationAgent;
        }

        public async Task Handle(SendSmsCodeConfirmationNotification notification, CancellationToken cancellationToken)
        {
            var request = new TransactionalSmsRequest(sender: notification.Sender,
                recipientPhoneNumber: notification.PhoneNumber,
                content: notification.Content);

            var response = await notificationAgent.SendSms(request);
            if (!response.Success)
                throw new DomainException(response.Message.ToString());
        }
    }
}