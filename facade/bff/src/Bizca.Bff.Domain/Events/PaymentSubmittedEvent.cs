namespace Bizca.Bff.Domain.Events
{
    using Core.Domain;
    using ValueObject;

    public sealed class PaymentSubmittedEvent : IEvent
    {
        public PaymentSubmittedEvent(string subscriptionReference,
            Money money)
        {
            SubscriptionCode = subscriptionReference;
            Money = money;
        }

        public string SubscriptionCode { get; }
        public Money Money { get; }
    }
}