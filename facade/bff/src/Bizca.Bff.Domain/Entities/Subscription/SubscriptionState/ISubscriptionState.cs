namespace Bizca.Bff.Domain.Entities.Subscription.SubscriptionState
{
    using Enumerations;

    public interface ISubscriptionState
    {
        SubscriptionStatus Status { get; }
        void StatusChangeCheck();
    }
}