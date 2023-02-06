namespace Bizca.Bff.Domain.Events
{
    using Core.Domain;
    using Core.Domain.Cqrs.Events;
    using System.Collections.Generic;

    public sealed class UserContactUpdatedNotificationEvent : INotificationEvent
    {
        public UserContactUpdatedNotificationEvent(string email,
            HashSet<int> unlinkListIds = default,
            HashSet<int> listIds = default,
            IDictionary<string, object> attributes = default)
        {
            UnlinkListIds = unlinkListIds;
            Attributes = attributes;
            ListIds = listIds;
            Email = email;
        }

        public IDictionary<string, object> Attributes { get; }
        public IReadOnlyCollection<int> UnlinkListIds { get; }
        public IReadOnlyCollection<int> ListIds { get; }
        public string Email { get; }
    }
}