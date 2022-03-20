namespace Bizca.Bff.Domain.Wrappers.Notification.Requests.Email
{
    using System.Collections.Generic;
    public sealed class TransactionalEmailRequest
    {
        public TransactionalEmailRequest(MailAddressRequest sender,
            ICollection<MailAddressRequest> to,
            IDictionary<string, string> parameters,
            int? emailTemplateId,
            string subject = null,
            string htmlContent = null)
        {
            TemplateId = emailTemplateId;
            HtmlContent = htmlContent;
            Params = parameters;
            Subject = subject;
            Sender = sender;
            To = to;
        }

        public IDictionary<string, string> Params { get; }
        public ICollection<MailAddressRequest> To { get; }
        public MailAddressRequest Sender { get; }
        public int? TemplateId { get; }
        public string HtmlContent { get; }
        public string Subject { get; }
    }
}