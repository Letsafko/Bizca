namespace Bizca.Bff.Domain.Entities.User.Events
{
    using Bizca.Core.Domain;
    public sealed class UserCreatedNotification : IEvent
    {
        public UserCreatedNotification(string externalUserId)
        {
            ExternalUserId = externalUserId;
        }

        public string ExternalUserId { get; }
    }
}
