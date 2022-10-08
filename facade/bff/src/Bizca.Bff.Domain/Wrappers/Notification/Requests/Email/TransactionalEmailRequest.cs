namespace Bizca.Bff.Domain.Wrappers.Notification.Requests.Email
{
    using System.Collections.Generic;

    public sealed class TransactionalEmailRequest
    {
        private Dictionary<string, object> _params;

        public TransactionalEmailRequest()
        {
            To = new List<MailAddressRequest>();
        }

        public IReadOnlyDictionary<string, object> Params => _params;
        public List<MailAddressRequest> To { get; set; }
        public MailAddressRequest Sender { get; set; }
        public int? TemplateId { get; set; }
        public string HtmlContent { get; set; }
        public string Subject { get; set; }

        public void AddNewParam(string key, object value)
        {
            _params ??= new Dictionary<string, object>();
            _params[key] = value;
        }

        public void AddParameters(IDictionary<string, object> parameters)
        {
            if (parameters is null || parameters.Count < 1) return;

            foreach (KeyValuePair<string, object> p in parameters) AddNewParam(p.Key, p.Value);
        }
    }
}