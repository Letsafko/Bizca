namespace Bizca.Bff.Domain.Events
{
    using Bizca.Core.Domain;
    public sealed class UserCreatedEvent : IEvent
    {
        public UserCreatedEvent(string externalUserId)
        {
            ExternalUserId = externalUserId;
        }

        public string ExternalUserId { get; }
    }
}
