namespace Bizca.Bff.Domain.Entities.Subscription.SubscriptionState
{
    using Bizca.Bff.Domain.Entities.Enumerations.Subscription;
    using Bizca.Bff.Domain.Entities.Subscription.Exceptions;
    using Bizca.Bff.Domain.Properties;
    using System.Collections.Generic;
    public sealed class Deactivated : SubscriptionStateBase, ISubscriptionState
    {
        public Deactivated(Subscription subscription) : base(subscription)
        {
        }
        protected override ICollection<SubscriptionStatus> AllowedNextStatus => new List<SubscriptionStatus>
        {
            SubscriptionStatus.Deactivated,
            SubscriptionStatus.Activated,
            SubscriptionStatus.Expired
        };
        public SubscriptionStatus Status => SubscriptionStatus.Deactivated;
        protected override ISubscriptionState ComputeStatus()
        {
            if (IsSubscriptionDesactivated(Subscription))
            {
                return this;
            }
            else if (IsSubscriptionActivated(Subscription))
            {
                return new Activated(Subscription);
            }
            else if (IsSubscriptionExpired(Subscription))
            {
                return new Expired(Subscription);
            }
            throw new UnSupportedSubscriptionStatusException(Resources.SUBSCRIPTION_STATUS_UNSUPPORTED);
        }
    }
}