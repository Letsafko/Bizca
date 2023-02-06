namespace Bizca.Bff.Domain.Events
{
    using Core.Domain;
    using Core.Domain.Cqrs.Events;
    using Wrappers;

    public sealed class SendTransactionalSmsNotificationEvent : INotificationEvent
    {
        public SendTransactionalSmsNotificationEvent(string sender,
            string recipientPhoneNumber,
            string content)
        {
            Recipient = recipientPhoneNumber;
            Content = content;
            Sender = sender;
        }

        public string TransactionType => Constants.TransactionType.Transactional;
        public string Recipient { get; }
        public string Content { get; }
        public string Sender { get; }
    }
}