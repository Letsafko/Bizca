namespace Bizca.Bff.Domain.Events
{
    using Bizca.Core.Domain;

    public sealed class PaymentExecutedEvent : IEvent
    {
        public string SubscriptionCode { get; }
        public PaymentExecutedEvent(string subscriptionCode)
        {
            SubscriptionCode = subscriptionCode;
        }
    }
}
