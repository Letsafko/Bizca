namespace Bizca.Bff.Domain.Entities.Subscription.SubscriptionState
{
    using Enumerations;
    using Exceptions;
    using Properties;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class SubscriptionStateBase
    {
        protected SubscriptionStateBase(Subscription subscription)
        {
            Subscription = subscription ?? throw new ArgumentNullException(nameof(subscription));
        }

        protected abstract ICollection<SubscriptionStatus> AllowedNextStatus { get; }
        protected Subscription Subscription { get; }
        protected abstract ISubscriptionState ComputeStatus();

        public void StatusChangeCheck()
        {
            ISubscriptionState newSubscriptionStatus = ComputeStatus();
            if (!AllowedNextStatus.Any(p => p == newSubscriptionStatus.Status))
                throw new SubscriptionStatusTransitionException
                (
                    string.Format(
                        Resources.SUBSCRIPTION_STATUS_OPERATION_UNAUTHORIZED,
                        Subscription.SubscriptionState.Status,
                        newSubscriptionStatus.Status)
                );
            Subscription.SetSubscriptionState(newSubscriptionStatus);
        }

        #region private helpers

        private bool IsSubscriptionActive(Subscription subscription)
        {
            return !(subscription.Bundle is null) &&
                   !subscription.SubscriptionSettings.IsFreeze.HasValue &&
                   subscription.SubscriptionSettings.BeginDate.HasValue &&
                   subscription.SubscriptionSettings.EndDate.HasValue &&
                   DateTime.UtcNow.CompareTo(Subscription.SubscriptionSettings.BeginDate) > 0 &&
                   DateTime.UtcNow.CompareTo(Subscription.SubscriptionSettings.EndDate) < 0;
        }

        #endregion

        #region protected helpers

        protected bool IsSubscriptionPaymentSubmitted(Subscription subscription)
        {
            return !(subscription.Bundle is null) &&
                   !subscription.SubscriptionSettings.BeginDate.HasValue &&
                   !subscription.SubscriptionSettings.EndDate.HasValue;
        }

        protected bool IsSubscriptionDesactivated(Subscription subscription)
        {
            return IsSubscriptionActive(subscription) &&
                   Subscription.SubscriptionSettings.IsFreeze.HasValue &&
                   Subscription.SubscriptionSettings.IsFreeze.Value;
        }

        protected bool IsSubscriptionActivated(Subscription subscription)
        {
            return IsSubscriptionActive(subscription) &&
                   !Subscription.SubscriptionSettings.IsFreeze.HasValue;
        }

        protected bool IsSubscriptionPending(Subscription subscription)
        {
            return subscription.Bundle is null ||
                   !subscription.SubscriptionSettings.BeginDate.HasValue ||
                   !subscription.SubscriptionSettings.EndDate.HasValue;
        }

        protected bool IsSubscriptionExpired(Subscription subscription)
        {
            return subscription.SubscriptionSettings.EndDate.HasValue &&
                   DateTime.UtcNow.CompareTo(Subscription.SubscriptionSettings.EndDate) > 0;
        }

        #endregion
    }
}