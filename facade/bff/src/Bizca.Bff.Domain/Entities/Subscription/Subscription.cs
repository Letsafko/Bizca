namespace Bizca.Bff.Domain.Entities.Subscription
{
    using Bizca.Bff.Domain.Entities.Enumerations.Subscription;
    using Bizca.Bff.Domain.Entities.Subscription.Exceptions;
    using Bizca.Bff.Domain.Entities.Subscription.SubscriptionState;
    using Bizca.Bff.Domain.Referentials.Bundle;
    using Bizca.Bff.Domain.Referentials.Bundle.ValueObjects;
    using Bizca.Bff.Domain.Referentials.Procedure;
    using Bizca.Core.Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class Subscription : Entity
    {
        public Subscription(Guid subscriptionCode,
            UserSubscription userSubscription,
            Procedure procedure,
            Bundle bundle,
            Money price,
            SubscriptionSettings subscriptionSettings,
            SubscriptionStatus subscriptionStatus = SubscriptionStatus.Pending)
        {
            SubscriptionCode = subscriptionCode;
            SubscriptionSettings = subscriptionSettings ?? throw new ArgumentNullException(nameof(subscriptionSettings));
            UserSubscription = userSubscription ?? throw new ArgumentNullException(nameof(userSubscription));
            Procedure = procedure ?? throw new ArgumentNullException(nameof(procedure));
            SubscriptionState = GetSubscriptionState(subscriptionStatus);
            CheckSum = ComputeCheckSum();
            Bundle = bundle;
            Price = price;
        }

        public Guid SubscriptionCode { get; }
        public ISubscriptionState SubscriptionState { get; private set; }
        public SubscriptionSettings SubscriptionSettings { get; }
        public UserSubscription UserSubscription { get; }
        public Procedure Procedure { get; }
        internal int CheckSum { get; }
        public Bundle Bundle { get; }
        public Money Price { get; }

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
        private IEnumerable<object> GetAtomicValues()
        {
            yield return UserSubscription.PhoneNumber?.Trim();
            yield return UserSubscription.Whatsapp?.Trim();
            yield return UserSubscription.Email?.Trim();
            yield return Procedure.ProcedureType.Id;
            yield return Procedure.Organism.Id;
        }
        private int ComputeCheckSum()
        {
            return GetAtomicValues()
                .Select(x => x?.GetHashCode() ?? 0)
                .Aggregate((x, y) => x ^ y);
        }

        #endregion
    }
}