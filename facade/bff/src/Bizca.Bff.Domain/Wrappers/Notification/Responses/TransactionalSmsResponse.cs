namespace Bizca.Bff.Domain.Wrappers.Notification.Responses
{
    public sealed class TransactionalSmsResponse
    {
        public string Reference { get; set; }
        public string MessageId { get; set; }
        public int RemainingCredits { get; set; }
        public int UsedCredits { get; set; }
        public int SmsCount { get; set; }
    }
}