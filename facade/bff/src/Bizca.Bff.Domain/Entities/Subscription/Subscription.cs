namespace Bizca.Bff.Domain.Entities.Subscription
{
    using Bizca.Bff.Domain.Entities.Enumerations.Subscription;
    using Bizca.Bff.Domain.Entities.Subscription.Exceptions;
    using Bizca.Bff.Domain.Entities.Subscription.SubscriptionState;
    using Bizca.Bff.Domain.Referentials.Bundle;
    using Bizca.Bff.Domain.Referentials.Procedure;
    using Bizca.Core.Domain;
    using System;
    public sealed class Subscription : Entity
    {
        public Subscription(UserSubscription userSubscription,
            Procedure procedure,
            Bundle bundle,
            SubscriptionSettings subscriptionSettings,
            SubscriptionStatus subscriptionStatus = SubscriptionStatus.Pending)
        {
            SubscriptionSettings = subscriptionSettings ?? throw new ArgumentNullException(nameof(subscriptionSettings));
            UserSubscription = userSubscription ?? throw new ArgumentNullException(nameof(userSubscription));
            Procedure = procedure ?? throw new ArgumentNullException(nameof(procedure));
            SubscriptionState = GetSubscriptionState(subscriptionStatus);
            Bundle = bundle;
        }

        public ISubscriptionState SubscriptionState { get; private set; }
        public SubscriptionSettings SubscriptionSettings { get; }
        public UserSubscription UserSubscription { get; }
        public Procedure Procedure { get; }
        public Bundle Bundle { get; }

        internal void SetSubscriptionState(ISubscriptionState subscriptionState)
        {
            if(SubscriptionState != subscriptionState)
            {
                SubscriptionState = subscriptionState;
            }
        }
        internal void SetBeginDate(DateTime beginDate)
        {
            SubscriptionSettings.SetBeginDate(beginDate);
            SubscriptionState.StatusChangeCheck();
        }
        internal void SetEndDate(DateTime endDate)
        {
            SubscriptionSettings.SetEndDate(endDate);
            SubscriptionState.StatusChangeCheck();
        }
        internal void SetSubscriptionId(int id)
        {
            Id = id;
        }
        internal void UnFreeze()
        {
            SubscriptionSettings.UnFreeze();
            SubscriptionState.StatusChangeCheck();
        }
        internal void Freeze()
        {
            SubscriptionSettings.Freeze();
            SubscriptionState.StatusChangeCheck();
        }

        #region private helpers

        ISubscriptionState GetSubscriptionState(SubscriptionStatus subscriptionStatus)
        {
            return subscriptionStatus switch
            {
                SubscriptionStatus.Deactivated => new Deactivated(this),
                SubscriptionStatus.Activated => new Activated(this),
                SubscriptionStatus.Expired => new Expired(this),
                SubscriptionStatus.Pending => new Pending(this),
                _ => throw new UnSupportedSubscriptionStatusException("subscription status is not supported.")
            };
        }

        #endregion
    }
}