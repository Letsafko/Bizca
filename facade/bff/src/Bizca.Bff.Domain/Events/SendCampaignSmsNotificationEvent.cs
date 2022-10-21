namespace Bizca.Bff.Domain.Events
{
    using Core.Domain;
    using Core.Domain.Cqrs.Events;
    using Wrappers.Notification.Requests;

    public sealed class SendCampaignSmsNotificationEvent : INotificationEvent
    {
        public SendCampaignSmsNotificationEvent(string compaignName,
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