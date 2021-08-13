namespace Bizca.Bff.Domain.Entities.Subscription.SubscriptionState
{
    using Bizca.Bff.Domain.Entities.Enumerations.Subscription;
    using Bizca.Bff.Domain.Entities.Subscription.Exceptions;
    using Bizca.Bff.Domain.Properties;
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
        protected abstract ISubscriptionState ComputeStatus();
        protected Subscription Subscription { get; }
        public void StatusChangeCheck()
        {
            ISubscriptionState newSubscriptionStatus = ComputeStatus();
            if (!AllowedNextStatus.Any(p => p == newSubscriptionStatus.Status))
            {
                throw new SubscriptionStatusTransitionException
                (
                    string.Format(
                        Resources.SUBSCRIPTION_STATUS_OPERATION_UNAUTHORIZED,
                        Subscription.SubscriptionState.Status,
                        newSubscriptionStatus.Status)
                );
            }
            Subscription.SetSubscriptionState(newSubscriptionStatus);
        }

        #region protected helpers

        protected bool IsSubscriptionDesactivated(Subscription subscription)
        {
            return IsSubscriptionActive(subscription) &&
                   Subscription.SubscriptionSettings.IsFreeze.HasValue &&
                   Subscription.SubscriptionSettings.IsFreeze.Value;
        }
        protected bool IsSubscriptionActivated(Subscription subscription)
        {
            return IsSubscriptionActive(subscription) &&
                    (
                        !Subscription.SubscriptionSettings.IsFreeze.HasValue ||
                        !Subscription.SubscriptionSettings.IsFreeze.Value
                    );
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
                   DateTime.Now.CompareTo(Subscription.SubscriptionSettings.EndDate) > 0;
        }

        #endregion

        #region private helpers

        private bool IsSubscriptionActive(Subscription subscription)
        {
            return !(subscription.Bundle is null) &&
                   subscription.SubscriptionSettings.BeginDate.HasValue &&
                   subscription.SubscriptionSettings.EndDate.HasValue &&
                   DateTime.Now.CompareTo(Subscription.SubscriptionSettings.BeginDate) > 0 &&
                   DateTime.Now.CompareTo(Subscription.SubscriptionSettings.EndDate) < 0;
        }

        #endregion
    }
}