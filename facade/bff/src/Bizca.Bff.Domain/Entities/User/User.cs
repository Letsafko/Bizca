namespace Bizca.Bff.Domain.Entities.User
{
    using Bizca.Bff.Domain.Entities.User.Events;
    using Bizca.Bff.Domain.Entities.User.ValueObjects;
    using Bizca.Bff.Domain.Enumerations;
    using Bizca.Core.Domain;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class User : Entity
    {
        public User (int id,
            UserIdentifier userIdentifier,
            UserProfile userProfile,
            List<Subscription.Subscription> subscriptionsToAdd = null)
        {
            subscriptions = subscriptionsToAdd ?? new List<Subscription.Subscription>();
            userEvents = new List<IEvent>();
            UserIdentifier = userIdentifier;
            UserProfile = userProfile;
            Id = id;
        }

        public IReadOnlyCollection<Subscription.Subscription> Subscriptions => subscriptions.ToList();
        private readonly ICollection<Subscription.Subscription> subscriptions;

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

        public void RegisterNewChannelCodeConfirmation(string externalUserId, ChannelType channelType)
        {
            userEvents.Add(new RegisterChannelNotification(externalUserId, channelType));
        }
        public void AddSubscription(Subscription.Subscription subscription)
        {
            subscriptions.Add(subscription);
        }
        internal void SetRowVersion(byte[] value)
        {
            rowVersion = value;
        }

        #endregion
    }
}