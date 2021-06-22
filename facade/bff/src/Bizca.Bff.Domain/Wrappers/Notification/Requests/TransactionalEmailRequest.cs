namespace Bizca.Bff.Domain.Wrappers.Notification.Requests
{
    using System.Collections.Generic;
    public sealed class TransactionalEmailRequest
    {
        public TransactionalEmailRequest(MailAddressRequest sender,
            ICollection<MailAddressRequest> to,
            string subject,
            string htmlContent)
        {
            HtmlContent = htmlContent;
            Subject = subject;
            Sender = sender;
            To = to;
        }

        public ICollection<MailAddressRequest> To { get; }
        public MailAddressRequest Sender { get; }
        public string HtmlContent { get; }
        public string Subject { get; }
    }
}