namespace Bizca.Bff.Domain.Entities.User.Events
{
    using Bizca.Bff.Domain.Wrappers;
    using Bizca.Core.Domain;
    public sealed class SendSmsNotification : IEvent
    {
        public SendSmsNotification(string sender,
            string recipientPhoneNumber,
            string content,
            string transactionType = Constants.TransactionType.Transactional)
        {
            TransactionType = transactionType;
            Recipient = recipientPhoneNumber;
            Content = content;
            Sender = sender;
        }

        public string TransactionType { get; }
        public string Recipient { get; }
        public string Content { get; }
        public string Sender { get; }
    }
}
