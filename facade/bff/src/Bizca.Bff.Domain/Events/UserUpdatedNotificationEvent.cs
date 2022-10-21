namespace Bizca.Bff.Domain.Events
{
    using Core.Domain;
    using Core.Domain.Cqrs.Events;

    public sealed class UserUpdatedNotificationEvent : INotificationEvent
    {
        public UserUpdatedNotificationEvent(string externalUserId)
        {
            ExternalUserId = externalUserId;
        }

        public string ExternalUserId { get; }
    }
}