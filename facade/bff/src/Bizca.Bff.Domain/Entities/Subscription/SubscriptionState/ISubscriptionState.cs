namespace Bizca.Bff.Domain.Entities.Subscription.SubscriptionState
{
    using Bizca.Bff.Domain.Entities.Enumerations.Subscription;
    public interface ISubscriptionState
    {
        SubscriptionStatus Status { get; }
        void StatusChangeCheck();
    }
}