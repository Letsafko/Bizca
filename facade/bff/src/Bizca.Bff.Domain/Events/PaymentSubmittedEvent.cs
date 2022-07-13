namespace Bizca.Bff.Domain.Events
{
    using Bizca.Bff.Domain.ValueObject;
    using Bizca.Core.Domain;
    public sealed class PaymentSubmittedEvent : IEvent
    {
        public string SubscriptionCode { get; }
        public Money Money { get; }

        public PaymentSubmittedEvent(string subscriptionReference,
            Money money)
        {
            SubscriptionCode = subscriptionReference;
            Money = money;
        }
    }
}
