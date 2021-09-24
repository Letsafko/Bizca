namespace Bizca.Bff.Domain.Entities.User.Events
{
    using Bizca.Bff.Domain.Wrappers.Notification.Requests.Email;
    using Bizca.Core.Domain;
    using System.Collections.Generic;
    public sealed class SendEmailNotification : IEvent
    {
        public SendEmailNotification(MailAddressRequest sender,
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
