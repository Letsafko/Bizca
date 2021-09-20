namespace Bizca.Bff.Domain.Wrappers.Notification.Requests.Sms
{
    public sealed class TransactionalSmsRequest
    {
        public TransactionalSmsRequest(string sender,
            string recipientPhoneNumber,
            string content)
        {
            Recipient = recipientPhoneNumber;
            Content = content;
            Sender = sender;
        }

        public string Type = Constants.TransactionType.Transactional;
        public string Recipient { get; }
        public string Content { get; }
        public string Sender { get; }
    }
}