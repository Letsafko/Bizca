﻿namespace Bizca.Bff.Domain.Entities.Subscription.SubscriptionState
{
    using Bizca.Bff.Domain.Entities.Enumerations.Subscription;
    using Bizca.Bff.Domain.Entities.Subscription.Exceptions;
    using Bizca.Bff.Domain.Properties;
    using System.Collections.Generic;
    public sealed class PaymentSubmitted : SubscriptionStateBase, ISubscriptionState
    {
        public PaymentSubmitted(Subscription subscription) : base(subscription)
        {
        }
        protected override ICollection<SubscriptionStatus> AllowedNextStatus =>
            new List<SubscriptionStatus>
            {
                SubscriptionStatus.PaymentSubmitted,
                SubscriptionStatus.Activated
            };
        public SubscriptionStatus Status => SubscriptionStatus.Activated;
        protected override ISubscriptionState ComputeStatus()
        {
            if (IsSubscriptionPaymentSubmitted(Subscription))
            {
                return this;
            }
            else if (IsSubscriptionActivated(Subscription))
            {
                return new Activated(Subscription);
            }
            throw new UnSupportedSubscriptionStatusException(Resources.SUBSCRIPTION_STATUS_UNSUPPORTED);
        }
    }
}