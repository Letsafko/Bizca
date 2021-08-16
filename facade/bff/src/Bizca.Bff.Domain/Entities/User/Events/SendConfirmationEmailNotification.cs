namespace Bizca.Bff.Domain.Entities.User.Events
{
    using Bizca.Bff.Domain.Enumerations;
    using Bizca.Bff.Domain.Properties;
    using Bizca.Core.Domain;

    public sealed class SendConfirmationEmailNotification : IEvent
    {
        public SendConfirmationEmailNotification(string externalUserId, string email, string fullName)
        {
            ExternalUserId = externalUserId;
            FullName = fullName;
            Email = email;
        }

        public ChannelType ChannelType { get; } = ChannelType.Email;
        public string PartnerCode { get; } = Resources.PartnerCode;
        public string ExternalUserId { get; }
        public string FullName { get; }
        public string Email { get; }
    }
}