namespace Bizca.Bff.Domain.Entities.User.Events
{
    using Bizca.Core.Domain;
    public sealed class UserUpdatedNotification : IEvent
    {
        public UserUpdatedNotification(string externalUserId)
        {
            ExternalUserId = externalUserId;
        }

        public string ExternalUserId { get; }
    }
}
