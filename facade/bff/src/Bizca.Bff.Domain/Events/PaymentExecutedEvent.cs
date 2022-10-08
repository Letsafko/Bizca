namespace Bizca.Bff.Domain.Events
{
    using Core.Domain;

    public sealed class PaymentExecutedEvent : IEvent
    {
        public PaymentExecutedEvent(string subscriptionCode)
        {
            SubscriptionCode = subscriptionCode;
        }

        public string SubscriptionCode { get; }
    }
}