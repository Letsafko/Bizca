namespace Bizca.Bff.Domain.Entities.User.Events
{
    using Bizca.Bff.Domain.Enumerations;
    using Bizca.Core.Domain;

    public sealed class RegisterChannelNotification : IEvent
    {
        public RegisterChannelNotification(string externalUserId, ChannelType channelType)
        {
            ExternalUserId = externalUserId;
            ChannelType = channelType;
        }

        public string PartnerCode { get; } = "Bizca";
        public ChannelType ChannelType { get; }
        public string ExternalUserId { get; }
    }
}