namespace Bizca.Bff.Domain.Entities.Subscription.SubscriptionState
{
    using Enumerations;
    using Exceptions;
    using Properties;
    using System.Collections.Generic;

    public sealed class Activated : SubscriptionStateBase, ISubscriptionState
    {
        public Activated(Subscription subscription) : base(subscription)
        {
        }

        protected override ICollection<SubscriptionStatus> AllowedNextStatus =>
            new List<SubscriptionStatus>
            {
                SubscriptionStatus.Deactivated, SubscriptionStatus.Activated, SubscriptionStatus.Expired
            };

        public SubscriptionStatus Status => SubscriptionStatus.Activated;

        protected override ISubscriptionState ComputeStatus()
        {
            if (IsSubscriptionActivated(Subscription))
                return this;
            if (IsSubscriptionDesactivated(Subscription))
                return new Deactivated(Subscription);
            if (IsSubscriptionExpired(Subscription)) return new Expired(Subscription);
            throw new UnSupportedSubscriptionStatusException(Resources.SUBSCRIPTION_STATUS_UNSUPPORTED);
        }
    }
}