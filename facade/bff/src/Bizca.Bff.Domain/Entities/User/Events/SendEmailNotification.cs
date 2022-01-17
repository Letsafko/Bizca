namespace Bizca.Bff.Domain.Entities.User.Events
{
    using Bizca.Bff.Domain.Wrappers.Notification.Requests.Email;
    using Bizca.Core.Domain;
    using System.Collections.Generic;
    public sealed class SendEmailNotification : IEvent
    {
        public SendEmailNotification(MailAddressRequest sender,
            ICollection<MailAddressRequest> to,
            string subject = "",
            string htmlContent = "")
        {
            Params = new Dictionary<string, string>();
            HtmlContent = htmlContent;
            Subject = subject;
            Sender = sender;
            To = to;
        }

        public Dictionary<string, string> Params { get; private set; }
        public ICollection<MailAddressRequest> To { get; }
        public MailAddressRequest Sender { get; }
        public string HtmlContent { get; private set; }
        public string Subject { get; private set; }
        public int TemplateId { get; private set; }

        public void SetHtmlContent(string htmlContent) => HtmlContent = htmlContent;
        public void AddNewParam(string key, string value) => Params[key] = value;
        public void SetTemplate(int emailTemplate) => TemplateId = emailTemplate;
        public void SetSubject(string subject) => Subject = subject;
    }
}
