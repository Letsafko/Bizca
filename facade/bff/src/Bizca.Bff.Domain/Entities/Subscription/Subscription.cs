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
        public Subscription(int id,
            Guid subscriptionCode,
            UserSubscription userSubscription,
            Procedure procedure,
            Bundle bundle,
            Money price,
            SubscriptionSettings subscriptionSettings,
            SubscriptionStatus subscriptionStatus = SubscriptionStatus.Pending)
        {
            SubscriptionCode = subscriptionCode;
            UserSubscription = userSubscription ?? throw new ArgumentNullException(nameof(userSubscription));
            Procedure = procedure ?? throw new ArgumentNullException(nameof(procedure));
            SubscriptionState = GetSubscriptionState(subscriptionStatus);
            SubscriptionSettings = subscriptionSettings;
            Bundle = bundle;
            Price = price;
            Id = id;
        }

        private const int NumberOfDaysInWeek = 7;
        public Guid SubscriptionCode { get; }
        public SubscriptionSettings SubscriptionSettings { get; private set; }
        public ISubscriptionState SubscriptionState { get; private set; }
        public UserSubscription UserSubscription { get; }
        public Procedure Procedure { get; private set; }
        public Bundle Bundle { get; private set; }
        public Money Price { get; private set; }
        internal int CheckSum => ComputeCheckSum();

        internal void SetSubscriptionState(ISubscriptionState subscriptionState)
        {
            if (SubscriptionState != subscriptionState)
            {
                SubscriptionState = subscriptionState;
            }
        }
        internal void SetSubscriptionSettings(Bundle bundle)
        {
            if (bundle != null)
            {
                SubscriptionSettings = new SubscriptionSettings(SubscriptionSettings?.WhatsappCounter ?? 0,
                    SubscriptionSettings?.EmailCounter ?? 0,
                    SubscriptionSettings?.SmsCounter ?? 0,
                    bundle.BundleSettings.TotalWhatsapp,
                    bundle.BundleSettings.TotalEmail,
                    bundle.BundleSettings.TotalSms);
            }
        }
        internal void SetProcedure(Procedure procedure)
        {
            Procedure = procedure;
        }
        internal void SetBundle(Bundle bundle)
        {
            if (bundle != null)
            {
                Price = bundle.Price;
                Bundle = bundle;

                DateTime now = DateTime.UtcNow;
                SetEndDate(now.AddDays(bundle.BundleSettings.IntervalInWeeks * NumberOfDaysInWeek));
                SetBeginDate(now);
            }
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
        private void SetBeginDate(DateTime beginDate)
        {
            SubscriptionSettings.SetBeginDate(beginDate);
            SubscriptionState.StatusChangeCheck();
        }
        private void SetEndDate(DateTime endDate)
        {
            SubscriptionSettings.SetEndDate(endDate);
            SubscriptionState.StatusChangeCheck();
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