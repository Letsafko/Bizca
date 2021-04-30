namespace Bizca.Bff.Domain.Entities.Subscription
{
    using Bizca.Bff.Domain.Entities.Enumerations.Subscription;
    using Bizca.Bff.Domain.Entities.Subscription.Exceptions;
    using Bizca.Bff.Domain.Entities.Subscription.SubscriptionState;
    using Bizca.Bff.Domain.Referentials.Pricing;
    using Bizca.Bff.Domain.Referentials.Procedure;
    using Bizca.Core.Domain;
    using System;
    public sealed class Subscription : Entity, IAggregateRoot
    {
        public Subscription(Procedure procedure,
            User user,
            Bundle bundle,
            SubscriptionSettings subscriptionSettings,
            SubscriptionStatus subscriptionStatus = SubscriptionStatus.Pending)
        {
            SubscriptionSettings = subscriptionSettings ?? throw new ArgumentNullException(nameof(subscriptionSettings));
            Procedure = procedure ?? throw new ArgumentNullException(nameof(procedure));
            User = user ?? throw new ArgumentNullException(nameof(user));
            SubscriptionState = GetSubscriptionState(subscriptionStatus);
            Bundle = bundle;
        }

        public ISubscriptionState SubscriptionState { get; private set; }
        public SubscriptionSettings SubscriptionSettings { get; }
        public Procedure Procedure { get; }
        public Bundle Bundle { get; }
        public User User { get; }

        internal void SetRowVersion(byte[] value)
        {
            rowVersion = value;
        }
        public byte[] GetRowVersion()
        {
            return rowVersion;
        }
        private byte[] rowVersion;

        internal void SetSubscriptionState(ISubscriptionState subscriptionState)
        {
            if(SubscriptionState != subscriptionState)
            {
                SubscriptionState = subscriptionState;
            }
        }
        public void SetBeginDate(DateTime beginDate)
        {
            SubscriptionSettings.SetBeginDate(beginDate);
            SubscriptionState.StatusChangeCheck();
        }
        public void SetEndDate(DateTime endDate)
        {
            SubscriptionSettings.SetEndDate(endDate);
            SubscriptionState.StatusChangeCheck();
        }
        public void SetSubscriptionId(int id)
        {
            Id = id;
        }
        public void UnFreeze()
        {
            SubscriptionSettings.UnFreeze();
            SubscriptionState.StatusChangeCheck();
        }
        public void Freeze()
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