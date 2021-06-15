namespace Bizca.User.Application.UseCases.RegisterPassword
{
    using Bizca.Core.Application.Commands;

    public sealed class RegisterPasswordCommand : ICommand
    {
        public string ChannelResource { get; }
        public string PartnerCode { get; }
        public string Password { get; }
        public RegisterPasswordCommand(string partnerCode, string channelResource, string password)
        {
            ChannelResource = channelResource;
            PartnerCode = partnerCode;
            Password = password;
        }
    }
}