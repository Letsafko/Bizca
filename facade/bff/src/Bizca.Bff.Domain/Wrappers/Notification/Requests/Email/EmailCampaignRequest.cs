namespace Bizca.Bff.Domain.Wrappers.Notification.Requests.Email
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    public class EmailCampaignRequest
    {
        private Dictionary<string, object> _params;

        [DataMember(Name = "name")] public string CompaignName { get; }

        [DataMember(Name = "templateId")] public int EmailTemplateId { get; set; }

        public IReadOnlyDictionary<string, object> Params => _params;
        public MailAddressRequest Sender { get; set; }
        public Recipient Recipients { get; set; }
        public string Subject { get; }
    }
}