namespace Bizca.Bff.Domain.Entities.Subscription.SubscriptionState
{
    using Bizca.Bff.Domain.Entities.Enumerations.Subscription;
    using Bizca.Bff.Domain.Entities.Subscription.Exceptions;
    using Bizca.Bff.Domain.Properties;
    using System.Collections.Generic;
    public sealed class Pending : SubscriptionStateBase, ISubscriptionState
    {
        public Pending(Subscription subscription) : base(subscription)
        {
        }
        protected override ICollection<SubscriptionStatus> AllowedNextStatus =>
            new List<SubscriptionStatus>
            {
                SubscriptionStatus.PaymentSubmitted,
                SubscriptionStatus.Pending
            };
        public SubscriptionStatus Status => SubscriptionStatus.Pending;
        protected override ISubscriptionState ComputeStatus()
        {
            if (IsSubscriptionPending(Subscription))
            {
                return this;
            }
            else if (IsSubscriptionPaymentSubmitted(Subscription))
            {
                return new PaymentSubmitted(Subscription);
            }
            throw new UnSupportedSubscriptionStatusException(Resources.SUBSCRIPTION_STATUS_UNSUPPORTED);
        }
    }
}