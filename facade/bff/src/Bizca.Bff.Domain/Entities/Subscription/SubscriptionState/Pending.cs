namespace Bizca.Bff.Domain.Entities.Subscription.SubscriptionState
{
    using Enumerations.Subscription;
    using Exceptions;
    using Properties;
    using System.Collections.Generic;

    public sealed class Pending : SubscriptionStateBase, ISubscriptionState
    {
        public Pending(Subscription subscription) : base(subscription)
        {
        }

        protected override ICollection<SubscriptionStatus> AllowedNextStatus =>
            new List<SubscriptionStatus> { SubscriptionStatus.PaymentSubmitted, SubscriptionStatus.Pending };

        public SubscriptionStatus Status => SubscriptionStatus.Pending;

        protected override ISubscriptionState ComputeStatus()
        {
            if (IsSubscriptionPending(Subscription))
                return this;
            if (IsSubscriptionPaymentSubmitted(Subscription)) return new PaymentSubmitted(Subscription);
            throw new UnSupportedSubscriptionStatusException(Resources.SUBSCRIPTION_STATUS_UNSUPPORTED);
        }
    }
}