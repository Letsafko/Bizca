namespace Bizca.Bff.Application.UseCases.UpsertPassword
{
    using Bizca.Core.Application.Commands;
    public sealed class UpsertPasswordCommand : ICommand
    {
        public string ChannelResource { get; }
        public string PartnerCode { get; }
        public string Password { get; }
        public UpsertPasswordCommand(string partnerCode, string channelResource, string password)
        {
            ChannelResource = channelResource;
            PartnerCode = partnerCode;
            Password = password;
        }
    }
}
