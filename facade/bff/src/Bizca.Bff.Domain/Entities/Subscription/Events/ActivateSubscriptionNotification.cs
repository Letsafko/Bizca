namespace Bizca.Bff.Domain.Entities.Subscription.Events
{
    using Bizca.Core.Domain;

    public sealed class ActivateSubscriptionNotification : IEvent
    {
        public ActivateSubscriptionNotification(string subscriptionCode)
        {
            SubscriptionCode = subscriptionCode;
        }

        public string SubscriptionCode { get; }
    }
}
