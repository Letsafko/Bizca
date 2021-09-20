namespace Bizca.Bff.Domain.Wrappers.Users.Requests
{
    using Bizca.Bff.Domain.Enumerations;
    using Bizca.Bff.Domain.Properties;

    public sealed class RegisterUserConfirmationCodeRequest
    {
        public RegisterUserConfirmationCodeRequest(string externalUserId, ChannelType channel)
        {
            Channel = channel.ToString();
            ExternalUserId = externalUserId;
        }
        public string PartnerCode { get; } = Resources.PartnerCode;
        public string ExternalUserId { get; }
        public string Channel { get; }
    }
}