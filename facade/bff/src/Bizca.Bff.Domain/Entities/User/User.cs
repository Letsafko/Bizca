﻿namespace Bizca.Bff.Domain.Entities.User
{
    using Bizca.Bff.Domain.Entities.Enumerations.Subscription;
    using Bizca.Bff.Domain.Entities.Subscription;
    using Bizca.Bff.Domain.Entities.Subscription.Exceptions;
    using Bizca.Bff.Domain.Entities.User.Events;
    using Bizca.Bff.Domain.Entities.User.ValueObjects;
    using Bizca.Bff.Domain.Enumerations;
    using Bizca.Bff.Domain.Referentials.Bundle;
    using Bizca.Bff.Domain.Referentials.Procedure;
    using Bizca.Bff.Domain.Wrappers.Notification.Requests.Email;
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

        #region events

        public void RegisterSendEmailEvent(MailAddressRequest sender,
            ICollection<MailAddressRequest> to,
            string subject,
            string htmlContent)
        {
            userEvents.Add(new SendEmailNotification(sender,
                to,
                subject,
                htmlContent));
        }

        public void RegisterSendSmsEvent(string sender, string phoneNumber, string content)
        {
            userEvents.Add(new SendSmsNotification(sender,
                phoneNumber,
                content));
        }
        public void RegisterUserCreatedEvent(UserCreatedNotification userCreated)
        {
            userEvents.Add(userCreated);
        }
        public void RegisterUserUpdatedEvent(UserUpdatedNotification userUpdated)
        {
            userEvents.Add(userUpdated);
        }

        #endregion

        public Subscription UpdateSubscription(string subscriptionCode,
            Bundle bundle,
            Procedure procedure)
        {
            Subscription subscription = GetSubscriptionByCode(subscriptionCode, true);
            if (!IsSubscriptionAllowedToBeUpdated(subscription))
            {
                throw new SubscriptionCannotBeUpdatedException($"subscription status {subscription.SubscriptionState.Status} does not allowed changes.",
                    nameof(subscription.SubscriptionState));
            }

            subscription.UpdateSubscription(bundle, procedure);
            if (IsSubscriptionConflicting(subscription))
            {
                throw new SubscriptionCannotBeUpdatedException($"subscription {subscription.SubscriptionCode} is in conflict with another one with same final status.",
                    nameof(subscription.SubscriptionState));
            }

            RemoveSubscriptionsWithSameCheckSum(subscription);
            return subscription;
        }
        public Subscription GetSubscriptionByCode(string subscriptionCode, bool throwError = false)
        {
            var subscription = subscriptions
                .FirstOrDefault(x => x.SubscriptionCode == Guid.Parse(subscriptionCode));

            return throwError && subscription is null
                ? throw new SubscriptionDoesNotExistException(nameof(subscription), "no subscription found for the given reference.")
                : subscription;
        }
        public void SetChannelConfirmationStatus(string channelType)
        {
            if (UserProfile is null)
                return;

            var channelToConfirm = GetChannelToConfirm(channelType);
            UserProfile.SetChannelConfirmationStatus(channelToConfirm);
        }
        public void SetChannelActivationStatus(string channelType)
        {
            if (UserProfile is null)
                return;

            var channelToActivate = GetChannelToActivate(channelType);
            UserProfile.SetChannelActivationStatus(channelToActivate);
        }
        public void RemoveSubscriptionsWithSameCheckSum(Subscription subscription)
        {
            subscriptions.RemoveAll(x => x.CheckSum == subscription.CheckSum
                && x.SubscriptionState.Status == SubscriptionStatus.Pending
                && x.SubscriptionCode != subscription.SubscriptionCode);
        }
        public Subscription DesactivateSubscription(string subscriptionCode)
        {
            Subscription subscription = GetSubscriptionByCode(subscriptionCode, true);
            if (IsAllowedToProcessDesactivation(subscription.SubscriptionState.Status))
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

        private bool IsSubscriptionConflicting(Subscription subscription)
        {
            return subscriptions
                .Where(x => x.CheckSum == subscription.CheckSum && x.SubscriptionState.Status != SubscriptionStatus.Expired)
                .Count() > 1;
        }

        private bool IsSubscriptionAllowedToBeUpdated(Subscription subscription)
        {
            return subscription.SubscriptionState.Status == SubscriptionStatus.Pending;
        }
        private ChannelConfirmationStatus GetChannelToConfirm(string channelType)
        {
            var channelTypeEnum = GetChannelType(channelType);
            return channelTypeEnum switch
            {
                ChannelType.Messenger => ChannelConfirmationStatus.MessengerConfirmed,
                ChannelType.Whatsapp => ChannelConfirmationStatus.WhatsappConfirmed,
                ChannelType.Sms => ChannelConfirmationStatus.PhoneNumberConfirmed,
                ChannelType.Email => ChannelConfirmationStatus.EmailConfirmed,
                _ => throw new InvalidCastException($"unable to retrieve channel confirmtion from channel type {channelTypeEnum}")
            };
        }
        private ChannelActivationStatus GetChannelToActivate(string channelType)
        {
            var channelTypeEnum = GetChannelType(channelType);
            return channelTypeEnum switch
            {
                ChannelType.Messenger => ChannelActivationStatus.MessengerActivated,
                ChannelType.Whatsapp => ChannelActivationStatus.WhatsappActivated,
                ChannelType.Sms => ChannelActivationStatus.PhoneNumberActivated,
                ChannelType.Email => ChannelActivationStatus.EmailActivated,
                _ => throw new InvalidCastException($"unable to retrieve channel activation from channel type {channelTypeEnum}")
            };
        }
        private bool IsAllowedToProcessActivation(SubscriptionStatus status)
        {
            return status == SubscriptionStatus.Deactivated;
        }
        private bool IsAllowedToProcessDesactivation(SubscriptionStatus status)
        {
            return status == SubscriptionStatus.Activated;
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
        private ChannelType GetChannelType(string channelType)
        {
            return Enum.Parse<ChannelType>(channelType, true);
        }
        private void SetRowVersion(byte[] value)
        {
            rowVersion = value;
        }

        #endregion
    }
}