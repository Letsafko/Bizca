namespace Bizca.Bff.Domain.Events
{
    using Core.Domain;
    using Core.Domain.Cqrs.Events;

    public sealed class PaymentExecutedNotificationEvent : INotificationEvent
    {
        public PaymentExecutedNotificationEvent(string subscriptionCode)
        {
            SubscriptionCode = subscriptionCode;
        }

        public string SubscriptionCode { get; }
    }
}