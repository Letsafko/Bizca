namespace Bizca.Bff.Domain.Events
{
    using Core.Domain;
    using Core.Domain.Cqrs.Events;
    using System.Collections.Generic;
    using Wrappers.Notification.Requests.Email;

    public sealed class SendTransactionalEmailNotificationEvent : INotificationEvent
    {
        public SendTransactionalEmailNotificationEvent(ICollection<MailAddressRequest> recipients,
            MailAddressRequest sender = default,
            IDictionary<string, object> parameters = default,
            int? emailTemplateId = default,
            string htmlContent = null,
            string subject = null)
        {
            TemplateId = emailTemplateId;
            HtmlContent = htmlContent;
            Recipients = recipients;
            Params = parameters;
            Subject = subject;
            Sender = sender;
        }

        public ICollection<MailAddressRequest> Recipients { get; }
        public IDictionary<string, object> Params { get; }
        public MailAddressRequest Sender { get; }
        public string HtmlContent { get; }
        public int? TemplateId { get; }
        public string Subject { get; }
    }
}