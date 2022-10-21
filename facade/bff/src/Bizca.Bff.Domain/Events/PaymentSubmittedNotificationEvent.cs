namespace Bizca.Bff.Domain.Events
{
    using Core.Domain;
    using Core.Domain.Cqrs.Events;
    using ValueObject;

    public sealed class PaymentSubmittedNotificationEvent : INotificationEvent
    {
        public PaymentSubmittedNotificationEvent(string subscriptionReference,
            Money money)
        {
            SubscriptionCode = subscriptionReference;
            Money = money;
        }

        public string SubscriptionCode { get; }
        public Money Money { get; }
    }
}