namespace Bizca.Bff.Domain.Entities.User
{
    using Bizca.Bff.Domain.Entities.Enumerations.Subscription;
    using Bizca.Bff.Domain.Entities.Subscription;
    using Bizca.Bff.Domain.Entities.Subscription.Exceptions;
    using Bizca.Bff.Domain.Entities.User.Events;
    using Bizca.Bff.Domain.Entities.User.ValueObjects;
    using Bizca.Bff.Domain.Enumerations;
    using Bizca.Bff.Domain.Referentials.Bundle;
    using Bizca.Bff.Domain.Referentials.Procedure;
    using Bizca.Core.Domain;
    using Bizca.Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class User : Entity
    {
        public User(int id,
            UserIdentifier userIdentifier,
            UserProfile userProfile,
            byte[] rowVersion = null)
        {
            subscriptions = new List<Subscription>();
            userEvents = new List<IEvent>();
            UserIdentifier = userIdentifier;
            UserProfile = userProfile;
            SetRowVersion(rowVersion);
            Id = id;
        }

        public IReadOnlyCollection<Subscription> Subscriptions => subscriptions.ToList();
        private readonly ICollection<Subscription> subscriptions;

        public IReadOnlyCollection<IEvent> UserEvents => userEvents.ToList();
        private readonly ICollection<IEvent> userEvents;

        public UserIdentifier UserIdentifier { get; }
        public UserProfile UserProfile { get; }

        public byte[] GetRowVersion()
        {
            return rowVersion;
        }
        private byte[] rowVersion;

        #region aggregate helpers

        public void RegisterSendConfirmationEmailEvent(string externalUserId, string email, string fullName)
        {
            userEvents.Add(new SendConfirmationEmalNotification(externalUserId, email, fullName));
        }
        public void UpdateSubscription(string subscriptionCode, Bundle bundle, Procedure procedure)
        {
            Subscription subscription = GetSubscriptionByCode(subscriptionCode);
            if (subscription is null)
            {
                throw new SubscriptionDoesNotExistException(nameof(subscription), "no subscription found for the given reference.");
            }

            if (!IsSubscriptionAllowedToBeUpdated(subscription.SubscriptionState.Status))
            {
                throw new SubscriptionCannotBeUpdatedException(nameof(subscription.SubscriptionState),
                    $"subscription status {subscription.SubscriptionState.Status} does not allowed changes.");
            }

            subscription.SetSubscriptionSettings(bundle);
            subscription.SetProcedure(procedure);
            subscription.SetBundle(bundle);
        }
        public void SetChannelConfirmationStatus(ChannelConfirmationStatus confirmationStatus)
        {
            if (UserProfile is null)
                return;

            UserProfile.SetChannelConfirmationStatus(confirmationStatus);
        }
        public void SetChannelActivationStatus(ChannelActivationStatus activationStatus)
        {
            if (UserProfile is null)
                return;

            UserProfile.SetChannelActivationStatus(activationStatus);
        }
        public Subscription GetSubscriptionByCode(string subscriptionCode)
        {
            return subscriptions.FirstOrDefault(x => x.SubscriptionCode.ToString().Equals(subscriptionCode, StringComparison.OrdinalIgnoreCase));
        }
        public void AddSubscription(Subscription subscription)
        {
            if (!IsSubscriptionAllowedToBeAdd(subscription))
                throw new DomainException(nameof(subscription), "subscription with same checksum already exists.");

            subscriptions.Add(subscription);
        }

        #endregion

        #region private helpers

        private bool IsSubscriptionAllowedToBeUpdated(SubscriptionStatus subscriptionStatus)
        {
            return subscriptionStatus == SubscriptionStatus.Pending;
        }
        private bool IsSubscriptionAllowedToBeAdd(Subscription subscription)
        {
            foreach (Subscription subscr in subscriptions)
            {
                if (subscr.CheckSum == subscription.CheckSum &&
                   subscr.SubscriptionState.Status != SubscriptionStatus.Expired)
                {
                    return false;
                }
            }
            return true;
        }
        private void SetRowVersion(byte[] value)
        {
            rowVersion = value;
        }

        #endregion
    }
}