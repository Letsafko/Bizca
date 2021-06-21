namespace Bizca.Bff.Domain.Wrappers.Users.Requests
{
    using Bizca.Bff.Domain.Enumerations;
    using Bizca.Bff.Domain.Properties;

    public sealed class RegisterUserConfirmationCodeRequest
    {
        public RegisterUserConfirmationCodeRequest(ChannelType channel)
        {
            Channel = channel.ToString();
        }
        public string PartnerCode { get; } = Resources.PartnerCode;
        public string Channel { get; }
    }
}