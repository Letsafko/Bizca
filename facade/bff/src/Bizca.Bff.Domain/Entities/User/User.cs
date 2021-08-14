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
            Role role,
            List<Subscription> subscriptions = null,
            byte[] rowVersion = null)
        {
            this.subscriptions = subscriptions ?? new List<Subscription>();
            userEvents = new List<IEvent>();
            UserIdentifier = userIdentifier;
            UserProfile = userProfile;
            SetRowVersion(rowVersion);
            Role = role;
            Id = id;
        }

        public IReadOnlyCollection<Subscription> Subscriptions => subscriptions.ToList();
        private readonly List<Subscription> subscriptions;

        public IReadOnlyCollection<IEvent> UserEvents => userEvents.ToList();
        private readonly ICollection<IEvent> userEvents;

        public UserIdentifier UserIdentifier { get; }
        public UserProfile UserProfile { get; }
        public Role Role { get; }

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
        public Subscription UpdateSubscription(string subscriptionCode, Bundle bundle, Procedure procedure)
        {
            Subscription subscription = GetSubscriptionByCode(subscriptionCode, true);
            if (!IsSubscriptionAllowedToBeUpdated(subscription.SubscriptionState.Status))
            {
                throw new SubscriptionCannotBeUpdatedException(nameof(subscription.SubscriptionState),
                    $"subscription status {subscription.SubscriptionState.Status} does not allowed changes.");
            }

            subscription.UpdateSubscription(bundle, procedure);
            RemoveSubscriptionsWithSameCheckSum(subscription);
            return subscription;
        }
        public Subscription GetSubscriptionByCode(string subscriptionCode, bool throwError = false)
        {
            var subscription = subscriptions
                .FirstOrDefault(x => x.SubscriptionCode.ToString().Equals(subscriptionCode,
                    StringComparison.OrdinalIgnoreCase));

            return throwError && subscription is null
                ? throw new SubscriptionDoesNotExistException(nameof(subscription), "no subscription found for the given reference.")
                : subscription;
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
        public void RemoveSubscriptionsWithSameCheckSum(Subscription subscription)
        {
            subscriptions.RemoveAll(x => x.CheckSum == subscription.CheckSum
                && x.SubscriptionState.Status == SubscriptionStatus.Pending
                && x.SubscriptionCode != subscription.SubscriptionCode);
        }
        public void RegisterUserCreatedEvent(UserCreatedNotification userCreated)
        {
            userEvents.Add(userCreated);
        }
        public void RegisterUserUpdatedEvent(UserUpdatedNotification userUpdated)
        {
            userEvents.Add(userUpdated);
        }
        public Subscription DesactivateSubscription(string subscriptionCode)
        {
            Subscription subscription = GetSubscriptionByCode(subscriptionCode, true);
            if (IsAllowedToProcessActivation(subscription.SubscriptionState.Status))
            {
                subscription?.Freeze();
            }
            return subscription;
        }
        public Subscription ActivateSubscription(string subscriptionCode)
        {
            Subscription subscription = GetSubscriptionByCode(subscriptionCode, true);
            if (IsAllowedToProcessActivation(subscription.SubscriptionState.Status))
            {
                subscription?.UnFreeze();
            }
            return subscription;
        }
        public void AddSubscription(Subscription subscription)
        {
            if (!IsSubscriptionAllowedToBeAdd(subscription))
                throw new DomainException(nameof(subscription), "subscription with same checksum already exists.");

            subscriptions.Add(subscription);
        }
        public void UpdateUserProfile(Civility? civility,
            string firstName,
            string lastName,
            string phoneNumber,
            string whatsapp,
            string email)
        {
            UserProfile.FirstName = !string.IsNullOrWhiteSpace(firstName) ? firstName : UserProfile.FirstName;
            UserProfile.LastName = !string.IsNullOrWhiteSpace(lastName) ? lastName : UserProfile.LastName;
            UserProfile.Civility = civility ?? UserProfile.Civility;

            if (!string.IsNullOrWhiteSpace(phoneNumber) &&
               !phoneNumber.Equals(UserProfile.PhoneNumber, StringComparison.OrdinalIgnoreCase))
            {
                UserProfile.RemoveChannelConfirmationStatus(ChannelConfirmationStatus.PhoneNumberConfirmed);
                UserProfile.PhoneNumber = phoneNumber;
            }

            if (!string.IsNullOrWhiteSpace(whatsapp) &&
               !whatsapp.Equals(UserProfile.Whatsapp, StringComparison.OrdinalIgnoreCase))
            {
                UserProfile.RemoveChannelConfirmationStatus(ChannelConfirmationStatus.WhatsappConfirmed);
                UserProfile.Whatsapp = whatsapp;
            }

            if (!string.IsNullOrWhiteSpace(email) &&
               !email.Equals(UserProfile.Email, StringComparison.OrdinalIgnoreCase))
            {
                UserProfile.RemoveChannelConfirmationStatus(ChannelConfirmationStatus.EmailConfirmed);
                UserProfile.Email = email;
            }
        }

        #endregion

        #region private helpers

        private ChannelStatus SetChannelConfirmationStatus(ChannelStatus channelStatus, ChannelConfirmationStatus confirmationStatus)
        {
            if (!channelStatus.ChannelConfirmationStatus.HasFlag(confirmationStatus))
            {
                var channelConfirmationStatus = channelStatus.ChannelConfirmationStatus | confirmationStatus;
                return new ChannelStatus(channelConfirmationStatus,
                    channelStatus.ChannelActivationStatus);
            }
            return channelStatus;
        }
        private ChannelStatus SetChannelActivationStatus(ChannelStatus channelStatus, ChannelActivationStatus activationStatus)
        {
            if (!channelStatus.ChannelActivationStatus.HasFlag(activationStatus))
            {
                var channelActivationStatus = channelStatus.ChannelActivationStatus | activationStatus;
                return new ChannelStatus(channelStatus.ChannelConfirmationStatus,
                    channelActivationStatus);
            }
            return channelStatus;
        }
        private bool IsSubscriptionAllowedToBeUpdated(SubscriptionStatus subscriptionStatus)
        {
            return subscriptionStatus == SubscriptionStatus.Pending;
        }
        private bool IsAllowedToProcessActivation(SubscriptionStatus status)
        {
            return status == SubscriptionStatus.Activated ||
                   status == SubscriptionStatus.Deactivated;
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