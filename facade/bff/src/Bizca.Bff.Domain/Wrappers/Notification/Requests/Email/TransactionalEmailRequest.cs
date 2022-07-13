namespace Bizca.Bff.Domain.Wrappers.Notification.Requests.Email
{
    using System.Collections.Generic;
    public sealed class TransactionalEmailRequest
    {
        public TransactionalEmailRequest()
        {
            To = new List<MailAddressRequest>();
        }

        private Dictionary<string, object> _params;
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
            if (parameters is null || parameters.Count < 1)
            {
                return;
            }

            foreach (var p in parameters)
            {
                AddNewParam(p.Key, p.Value);
            }
        }
    }
}