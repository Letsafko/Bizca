namespace Bizca.Bff.Domain.Entities.User
{
    using Core.Domain;
    using Core.Domain.Exceptions;
    using Domain.Enumerations;
    using Enumerations.Subscription;
    using Events;
    using Referentials.Bundle;
    using Referentials.Procedure;
    using Subscription;
    using Subscription.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ValueObjects;
    using Wrappers.Notification.Requests.Email;

    public sealed class User : Entity
    {
        private readonly List<Subscription> subscriptions;
        private readonly ICollection<IEvent> userEvents;

        private byte[] rowVersion;

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

        public IReadOnlyCollection<IEvent> UserEvents => userEvents.ToList();

        public UserIdentifier UserIdentifier { get; }
        public UserProfile UserProfile { get; }
        public Role Role { get; }

        public byte[] GetRowVersion()
        {
            return rowVersion;
        }

        #region events

        public void RegisterUserContactToCreateEvent()
        {
            var attributes = new Dictionary<string, object>();
            if (!string.IsNullOrWhiteSpace(UserProfile.PhoneNumber))
                attributes.AddNewPair(AttributeConstant.Contact.PhoneNumber, UserProfile.PhoneNumber);

            if (!string.IsNullOrWhiteSpace(UserProfile.FirstName))
                attributes.AddNewPair(AttributeConstant.Contact.FirstName, UserProfile.FirstName);

            if (!string.IsNullOrWhiteSpace(UserProfile.LastName))
                attributes.AddNewPair(AttributeConstant.Contact.LastName, UserProfile.LastName);

            attributes.AddNewPair(AttributeConstant.Contact.Civility, UserProfile.Civility);
            attributes.AddNewPair(AttributeConstant.Contact.Email, UserProfile.Email);
            var notification = new UserContactToCreateEvent(UserIdentifier.PartnerCode,
                UserProfile.Email,
                attributes);

            userEvents.Add(notification);
        }

        internal void RegisterUserContactToUpdateEvent(UserContactUpdatedEvent updateUserContactNotification)
        {
            userEvents.Add(updateUserContactNotification);
        }

        public void RegisterSendTransactionalEmailEvent(ICollection<MailAddressRequest> recipients,
            MailAddressRequest sender = default,
            int? emailTemplate = default,
            IDictionary<string, object> parameters = default,
            string htmlContent = default,
            string subject = default)
        {
            var @event = new SendTransactionalEmailEvent(recipients,
                sender,
                parameters,
                emailTemplate,
                htmlContent,
                subject);

            userEvents.Add(@event);
        }

        public void RegisterPaymentExecutedEvent(string subscriptionCode)
        {
            var notification = new PaymentExecutedEvent(subscriptionCode);
            userEvents.Add(notification);
        }

        public void RegisterSendTransactionalSmsEvent(string sender,
            string phoneNumber,
            string content)
        {
            userEvents.Add(new SendTransactionalSmsEvent(sender,
                phoneNumber,
                content));
        }

        public void RegisterUserCreatedEvent(string externalUserId)
        {
            userEvents.Add(new UserCreatedEvent(externalUserId));
        }

        public void RegisterUserUpdatedEvent(string externalUserId)
        {
            userEvents.Add(new UserUpdatedEvent(externalUserId));
        }

        #endregion

        #region aggregate helpers

        public void UpdateSubscription(string subscriptionCode, Procedure procedure)
        {
            Subscription subscription = GetSubscriptionByCode(subscriptionCode, true);
            if (!IsSubscriptionAllowedToBeUpdated(subscription))
                throw new SubscriptionCannotBeUpdatedException(
                    $"subscription status {subscription.SubscriptionState.Status} does not allowed changes.",
                    nameof(subscription.SubscriptionState));

            subscription.UpdateProcedureSubscription(procedure);
            if (IsSubscriptionConflicting(subscription))
                throw new SubscriptionCannotBeUpdatedException(
                    $"subscription {subscription.SubscriptionCode} is in conflict with another one with same final status.",
                    nameof(subscription.SubscriptionState));

            RemoveSubscriptionsWithSameCheckSum(subscription);
        }

        public Subscription GetSubscriptionByCode(string subscriptionCode, bool throwError = false)
        {
            Subscription subscription = subscriptions
                .FirstOrDefault(x => x.SubscriptionCode == Guid.Parse(subscriptionCode));

            return throwError && subscription is null
                ? throw new SubscriptionDoesNotExistException(nameof(subscription),
                    "no subscription found for the given reference.")
                : subscription;
        }

        public void SetChannelConfirmationStatus(string channelType)
        {
            if (UserProfile is null)
                return;

            ChannelConfirmationStatus channelToConfirm = GetChannelToConfirm(channelType);
            UserProfile.SetChannelConfirmationStatus(channelToConfirm);
        }

        public void SetChannelActivationStatus(string channelType)
        {
            if (UserProfile is null)
                return;

            ChannelActivationStatus channelToActivate = GetChannelToActivate(channelType);
            UserProfile.SetChannelActivationStatus(channelToActivate);
        }

        public void RemoveSubscriptionsWithSameCheckSum(Subscription subscription)
        {
            //supprimer toutes les subscriptions en pending ayant le même checksum que l'instance courante.
            subscriptions.RemoveAll(x => x.CheckSum == subscription.CheckSum
                                         && x.SubscriptionState.Status == SubscriptionStatus.Pending
                                         && x.SubscriptionCode != subscription.SubscriptionCode);
        }

        public Subscription FreezeSubscription(string subscriptionCode)
        {
            Subscription subscription = GetSubscriptionByCode(subscriptionCode, true);
            if (IsAllowedToProcessDesactivation(subscription.SubscriptionState.Status)) subscription.Freeze();
            return subscription;
        }

        public Subscription UnFreezeSubscription(string subscriptionCode)
        {
            Subscription subscription = GetSubscriptionByCode(subscriptionCode, true);
            if (IsAllowedToProcessActivation(subscription.SubscriptionState.Status)) subscription.UnFreeze();
            return subscription;
        }

        public void UpdateSubscriptionBundle(string subscriptionCode, Bundle bundle)
        {
            Subscription subscription = GetSubscriptionByCode(subscriptionCode, true);
            subscription.UpdateSubscriptionBundle(bundle);
        }

        public void UpdateSubscriptionDateRange(string subscriptionCode)
        {
            Subscription subscription = GetSubscriptionByCode(subscriptionCode, true);
            subscription.UpdateSubscriptionDateRange();

            var activateUserEvent = new ActivateUserContactEvent(subscription.Procedure,
                UserIdentifier.PartnerCode,
                UserProfile.Email);

            userEvents.Add(activateUserEvent);
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
            var attributes = new Dictionary<string, object>();
            if (!string.IsNullOrWhiteSpace(firstName) &&
                !firstName.Equals(UserProfile.FirstName, StringComparison.OrdinalIgnoreCase))
            {
                attributes.AddNewPair(AttributeConstant.Contact.FirstName, firstName);
                UserProfile.FirstName = firstName;
            }

            if (!string.IsNullOrWhiteSpace(lastName) &&
                !lastName.Equals(UserProfile.LastName, StringComparison.OrdinalIgnoreCase))
            {
                attributes.AddNewPair(AttributeConstant.Contact.LastName, lastName);
                UserProfile.LastName = lastName;
            }

            if (civility.HasValue &&
                civility.Value != UserProfile.Civility)
            {
                attributes.AddNewPair(AttributeConstant.Contact.Civility, civility.Value);
                UserProfile.Civility = civility.Value;
            }

            if (!string.IsNullOrWhiteSpace(phoneNumber) &&
                !phoneNumber.Equals(UserProfile.PhoneNumber, StringComparison.OrdinalIgnoreCase))
            {
                attributes.AddNewPair(AttributeConstant.Contact.PhoneNumber, phoneNumber);
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
                attributes.AddNewPair(AttributeConstant.Contact.Email, email);
                UserProfile.RemoveChannelConfirmationStatus(ChannelConfirmationStatus.EmailConfirmed);
                UserProfile.Email = email;
            }

            if (attributes.Any())
            {
                var userContactToUpdateEvent = new UserContactUpdatedEvent(UserProfile.Email,
                    attributes: attributes);

                RegisterUserContactToUpdateEvent(userContactToUpdateEvent);
            }
        }

        #endregion

        #region private helpers

        private bool IsSubscriptionConflicting(Subscription subscription)
        {
            //verifier qu'il n'existe pas plus d'une subscription avec le même checksum et un status different de <Expired>
            return subscriptions
                .Where(x => x.CheckSum == subscription.CheckSum &&
                            x.SubscriptionState.Status != SubscriptionStatus.Expired)
                .Count() > 1;
        }

        private bool IsSubscriptionAllowedToBeUpdated(Subscription subscription)
        {
            return subscription.SubscriptionState.Status == SubscriptionStatus.Pending;
        }

        private ChannelConfirmationStatus GetChannelToConfirm(string channelType)
        {
            ChannelType channelTypeEnum = GetChannelType(channelType);
            return channelTypeEnum switch
            {
                ChannelType.Messenger => ChannelConfirmationStatus.MessengerConfirmed,
                ChannelType.Whatsapp => ChannelConfirmationStatus.WhatsappConfirmed,
                ChannelType.Sms => ChannelConfirmationStatus.PhoneNumberConfirmed,
                ChannelType.Email => ChannelConfirmationStatus.EmailConfirmed,
                _ => throw new InvalidCastException(
                    $"unable to retrieve channel confirmtion from channel type {channelTypeEnum}")
            };
        }

        private ChannelActivationStatus GetChannelToActivate(string channelType)
        {
            ChannelType channelTypeEnum = GetChannelType(channelType);
            return channelTypeEnum switch
            {
                ChannelType.Messenger => ChannelActivationStatus.MessengerActivated,
                ChannelType.Whatsapp => ChannelActivationStatus.WhatsappActivated,
                ChannelType.Sms => ChannelActivationStatus.PhoneNumberActivated,
                ChannelType.Email => ChannelActivationStatus.EmailActivated,
                _ => throw new InvalidCastException(
                    $"unable to retrieve channel activation from channel type {channelTypeEnum}")
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
                if (subscr.CheckSum == subscription.CheckSum &&
                    subscr.SubscriptionState.Status != SubscriptionStatus.Expired)
                    return false;
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