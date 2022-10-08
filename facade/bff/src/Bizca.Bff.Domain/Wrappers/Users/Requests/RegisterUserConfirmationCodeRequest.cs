namespace Bizca.Bff.Domain.Wrappers.Users.Requests
{
    using Enumerations;
    using Properties;

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