namespace Bizca.Bff.Domain.Wrappers.Notification.Responses
{
    public sealed class TransactionalSmsResponse
    {
        public string Reference { get; set; }
        public long MessageId { get; set; }
        public decimal RemainingCredits { get; set; }
        public decimal UsedCredits { get; set; }
        public int SmsCount { get; set; }
    }
}