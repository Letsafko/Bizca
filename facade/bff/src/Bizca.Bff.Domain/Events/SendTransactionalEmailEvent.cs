namespace Bizca.Bff.Domain.Events
{
    using Bizca.Bff.Domain.Wrappers.Notification.Requests.Email;
    using Bizca.Core.Domain;
    using System.Collections.Generic;

    public sealed class SendTransactionalEmailEvent : IEvent
    {
        public SendTransactionalEmailEvent(ICollection<MailAddressRequest> recipients,
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
