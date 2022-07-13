namespace Bizca.Bff.Application.UseCases.SendEmail
{
    using Bizca.Bff.Domain.Events;
    using Bizca.Bff.Domain.Wrappers.Notification;
    using Bizca.Bff.Domain.Wrappers.Notification.Requests.Email;
    using Bizca.Core.Application.Events;
    using Bizca.Core.Domain.Exceptions;
    using System.Threading;
    using System.Threading.Tasks;
    public sealed class SendTransactionalEmailUseCase : IEventHandler<SendTransactionalEmailEvent>
    {
        private readonly INotificationWrapper notificationAgent;
        public SendTransactionalEmailUseCase(INotificationWrapper notificationAgent)
        {
            this.notificationAgent = notificationAgent;
        }

        public async Task Handle(SendTransactionalEmailEvent notification, CancellationToken cancellationToken)
        {
            var request = TransactionalEmailRequestBuilder
                .Instance
                .WithEmailTemplate(notification.TemplateId)
                .WithHtmlContent(notification.HtmlContent)
                .WithRecipients(notification.Recipients)
                .WithParameters(notification.Params)
                .WithSubject(notification.Subject)
                .WithSender(notification.Sender)
                .Build();

            var response = await notificationAgent.SendTransactionalEmail(request);
            if (!response.Success)
                throw new DomainException(response.Message.ToString());
        }
    }
}
