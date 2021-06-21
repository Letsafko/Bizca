namespace Bizca.Bff.Domain.Wrappers.Notification.Requests
{
    public sealed class TransactionalEmailRequest
    {
        public TransactionalEmailRequest(string from,
            string to,
            string subject,
            string body)
        {
            Subject = subject;
            Body = body;
            From = from;
            To = to;
        }

        public string Subject { get; }
        public string Body { get; }
        public string From { get; }
        public string To { get; }
    }
}
