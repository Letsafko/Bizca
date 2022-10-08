namespace Bizca.Bff.Domain.Entities.Subscription.SubscriptionState
{
    using Enumerations.Subscription;

    public interface ISubscriptionState
    {
        SubscriptionStatus Status { get; }
        void StatusChangeCheck();
    }
}