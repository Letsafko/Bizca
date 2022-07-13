namespace Bizca.Bff.Domain.Wrappers.Notification.Requests.Email
{
    using System.Collections.Generic;

    public sealed class TransactionalEmailRequestBuilder
    {
        private readonly TransactionalEmailRequest _transactionalEmailRequest;
        private TransactionalEmailRequestBuilder()
        {
            _transactionalEmailRequest = new TransactionalEmailRequest();
        }

        public static TransactionalEmailRequestBuilder Instance
            => new TransactionalEmailRequestBuilder();

        public TransactionalEmailRequest Build()
        {
            return _transactionalEmailRequest;
        }

        public TransactionalEmailRequestBuilder WithParameters(IDictionary<string, object> parameters)
        {
            _transactionalEmailRequest.AddParameters(parameters);
            return this;
        }
        public TransactionalEmailRequestBuilder WithRecipients(IEnumerable<MailAddressRequest> recipients)
        {
            _transactionalEmailRequest.To.AddRange(recipients);
            return this;
        }
        public TransactionalEmailRequestBuilder WithSender(MailAddressRequest sender)
        {
            _transactionalEmailRequest.Sender = sender;
            return this;
        }
        public TransactionalEmailRequestBuilder WithEmailTemplate(int? emailTemplate)
        {
            _transactionalEmailRequest.TemplateId = emailTemplate;
            return this;
        }
        public TransactionalEmailRequestBuilder WithHtmlContent(string htmlContent)
        {
            _transactionalEmailRequest.HtmlContent = htmlContent;
            return this;
        }
        public TransactionalEmailRequestBuilder WithSubject(string subject)
        {
            _transactionalEmailRequest.Subject = subject;
            return this;
        }
    }
}
