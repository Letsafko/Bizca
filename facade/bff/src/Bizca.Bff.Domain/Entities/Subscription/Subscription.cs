namespace Bizca.Bff.Domain.Entities.Subscription
{
    using Bizca.Bff.Domain.Entities.Enumerations.Subscription;
    using Bizca.Bff.Domain.Entities.Subscription.Exceptions;
    using Bizca.Bff.Domain.Entities.Subscription.SubscriptionState;
    using Bizca.Bff.Domain.Referentials.Bundle;
    using Bizca.Bff.Domain.Referentials.Procedure;
    using Bizca.Bff.Domain.ValueObject;
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

        internal void UpdateProcedureSubscription(Procedure procedureToUpdate)
        {
            if (procedureToUpdate is null || procedureToUpdate == Procedure)
            {
                return;
            }

            Procedure = procedureToUpdate;
        }

        internal void UpdateSubscriptionBundle(Bundle bundleToAdd)
        {
            Bundle = bundleToAdd;
            Price = bundleToAdd.Price;
            SubscriptionSettings = new SubscriptionSettings(SubscriptionSettings?.WhatsappCounter ?? 0,
                SubscriptionSettings?.EmailCounter ?? 0,
                SubscriptionSettings?.SmsCounter ?? 0,
                bundleToAdd.BundleSettings.TotalWhatsapp,
                bundleToAdd.BundleSettings.TotalEmail,
                bundleToAdd.BundleSettings.TotalSms);
        }

        internal void UpdateSubscriptionDateRange()
        {
            DateTime beginDate = DateTime.UtcNow;
            DateTime endDate = beginDate.AddDays(Bundle.BundleSettings.IntervalInWeeks * NumberOfDaysInWeek);

            SetEndDate(endDate);
            SetBeginDate(beginDate);
        }

        internal void SetSubscriptionState(ISubscriptionState subscriptionState)
        {
            if (SubscriptionState.Status != subscriptionState.Status)
            {
                SubscriptionState = subscriptionState;
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
            yield return Procedure.Organism.CodeInsee;
            yield return Procedure.ProcedureType.Id;
        }
        private int ComputeCheckSum()
        {
            IEnumerable<object> values = GetAtomicValues().Where(x => x != null);
            return values.Select(x => x.GetHashCode())
                         .Aggregate((x, y) => x ^ y);
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

        #endregion
    }
}