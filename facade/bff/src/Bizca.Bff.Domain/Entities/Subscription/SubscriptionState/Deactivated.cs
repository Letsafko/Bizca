namespace Bizca.Bff.Domain.Entities.Subscription.SubscriptionState
{
    using Enumerations.Subscription;
    using Exceptions;
    using Properties;
    using System.Collections.Generic;

    public sealed class Deactivated : SubscriptionStateBase, ISubscriptionState
    {
        public Deactivated(Subscription subscription) : base(subscription)
        {
        }

        protected override ICollection<SubscriptionStatus> AllowedNextStatus => new List<SubscriptionStatus>
        {
            SubscriptionStatus.Deactivated, SubscriptionStatus.Activated, SubscriptionStatus.Expired
        };

        public SubscriptionStatus Status => SubscriptionStatus.Deactivated;

        protected override ISubscriptionState ComputeStatus()
        {
            if (IsSubscriptionDesactivated(Subscription))
                return this;
            if (IsSubscriptionActivated(Subscription))
                return new Activated(Subscription);
            if (IsSubscriptionExpired(Subscription)) return new Expired(Subscription);
            throw new UnSupportedSubscriptionStatusException(Resources.SUBSCRIPTION_STATUS_UNSUPPORTED);
        }
    }
}