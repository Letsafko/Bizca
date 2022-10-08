namespace Bizca.Bff.Domain.Wrappers.Notification.Requests.Sms
{
    using System.Runtime.Serialization;

    public class SmsCampaignRequest
    {
        public SmsCampaignRequest(string compaignName,
            Recipient recipient,
            string sender,
            string content)
        {
            CompaignName = compaignName;
            Recipient = recipient;
            Content = content;
            Sender = sender;
        }

        [DataMember(Name = "name")] public string CompaignName { get; }

        public bool UnicodeEnabled => true;
        public Recipient Recipient { get; }
        public string Content { get; }
        public string Sender { get; }
    }
}