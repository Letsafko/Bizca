using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Bizca.Bff.Domain.Wrappers.Notification.Requests.Email
{
    public class EmailCampaignRequest
    {
        [DataMember(Name = "name")]
        public string CompaignName { get; }

        [DataMember(Name = "templateId")]
        public int EmailTemplateId { get; set; }

        private Dictionary<string, object> _params;
        public IReadOnlyDictionary<string, object> Params => _params;
        public MailAddressRequest Sender { get; set; }
        public Recipient Recipients { get; set; }
        public string Subject { get; }
    }
}
