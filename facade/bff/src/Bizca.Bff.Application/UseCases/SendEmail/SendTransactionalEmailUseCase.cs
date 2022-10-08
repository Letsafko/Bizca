namespace Bizca.Bff.Application.UseCases.SendEmail
{
    using Core.Application.Events;
    using Core.Domain;
    using Core.Domain.Exceptions;
    using Domain.Events;
    using Domain.Wrappers.Notification;
    using Domain.Wrappers.Notification.Requests.Email;
    using Domain.Wrappers.Notification.Responses;
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
            TransactionalEmailRequest request = TransactionalEmailRequestBuilder
                .Instance
                .WithEmailTemplate(notification.TemplateId)
                .WithHtmlContent(notification.HtmlContent)
                .WithRecipients(notification.Recipients)
                .WithParameters(notification.Params)
                .WithSubject(notification.Subject)
                .WithSender(notification.Sender)
                .Build();

            IPublicResponse<TransactionalEmailResponse> response =
                await notificationAgent.SendTransactionalEmail(request);
            if (!response.Success)
                throw new DomainException(response.Message.ToString());
        }
    }
}