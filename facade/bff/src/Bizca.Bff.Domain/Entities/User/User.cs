namespace Bizca.Bff.Domain.Entities.User
{
    using Bizca.Bff.Domain.Entities.User.ValueObjects;
    using Bizca.Core.Domain;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class User : Entity
    {
        public User (UserIdentifier userIdentifier,
            UserProfile userProfile,
            List<Subscription.Subscription> subscriptionsToAdd = null)
        {
            subscriptions = subscriptionsToAdd ?? new List<Subscription.Subscription>();
            UserIdentifier = userIdentifier;
            UserProfile = userProfile;
        }

        public IReadOnlyCollection<Subscription.Subscription> Subscriptions => subscriptions.ToList();
        private readonly ICollection<Subscription.Subscription> subscriptions;
        public UserIdentifier UserIdentifier { get; }
        public UserProfile UserProfile { get; }
        
        public byte[] GetRowVersion()
        {
            return rowVersion;
        }
        private byte[] rowVersion;

        #region aggregate helpers

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