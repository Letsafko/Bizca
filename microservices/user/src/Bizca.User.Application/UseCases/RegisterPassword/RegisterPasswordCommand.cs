namespace Bizca.User.Application.UseCases.RegisterPassword
{
    using Core.Application.Commands;

    public sealed class RegisterPasswordCommand : ICommand
    {
        public RegisterPasswordCommand(string partnerCode, string channelResource, string password)
        {
            ChannelResource = channelResource;
            PartnerCode = partnerCode;
            Password = password;
        }

        public string ChannelResource { get; }
        public string PartnerCode { get; }
        public string Password { get; }
    }
}