namespace Bizca.Bff.Domain.Entities.Subscription.SubscriptionState
{
    using Bizca.Bff.Domain.Entities.Enumerations.Subscription;
    using Bizca.Bff.Domain.Entities.Subscription.Exceptions;
    using Bizca.Bff.Domain.Properties;
    using System.Collections.Generic;
    public sealed class Expired : SubscriptionStateBase, ISubscriptionState
    {
        public Expired(Subscription subscription) : base(subscription)
        {
        }
        protected override ICollection<SubscriptionStatus> AllowedNextStatus =>
            new List<SubscriptionStatus>
            {
                SubscriptionStatus.Expired
            };
        public SubscriptionStatus Status => SubscriptionStatus.Expired;
        protected override ISubscriptionState ComputeStatus()
        {
            return IsSubscriptionExpired(Subscription)
                ? this
                : throw new UnSupportedSubscriptionStatusException(Resources.SUBSCRIPTION_STATUS_UNSUPPORTED);
        }
    }
}