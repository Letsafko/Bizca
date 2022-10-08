namespace Bizca.Bff.Domain.Events
{
    using Core.Domain;

    public sealed class UserUpdatedEvent : IEvent
    {
        public UserUpdatedEvent(string externalUserId)
        {
            ExternalUserId = externalUserId;
        }

        public string ExternalUserId { get; }
    }
}