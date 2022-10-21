namespace Bizca.Bff.Domain.Events
{
    using Core.Domain;
    using Core.Domain.Cqrs.Events;

    public sealed class UserCreatedNotificationEvent : INotificationEvent
    {
        public UserCreatedNotificationEvent(string externalUserId)
        {
            ExternalUserId = externalUserId;
        }

        public string ExternalUserId { get; }
    }
}