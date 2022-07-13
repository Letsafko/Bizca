namespace Bizca.Bff.Domain.Events
{
    using Bizca.Bff.Domain.Wrappers.Notification.Requests;
    using Bizca.Core.Domain;
    public sealed class SendCampaignSmsEvent : IEvent
    {
        public SendCampaignSmsEvent(string compaignName,
            Recipient recipient,
            string sender,
            string content)
        {
            CompaignName = compaignName;
            Recipient = recipient;
            Content = content;
            Sender = sender;
        }

        public bool UnicodeEnabled => true;
        public Recipient Recipient { get; }
        public string CompaignName { get; }
        public string Content { get; }
        public string Sender { get; }
    }
}
