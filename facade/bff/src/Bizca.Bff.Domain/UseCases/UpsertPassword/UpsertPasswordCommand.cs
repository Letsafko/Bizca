namespace Bizca.Bff.Application.UseCases.UpsertPassword
{
    using Core.Domain.Cqrs.Commands;

    public sealed class UpsertPasswordCommand : ICommand
    {
        public UpsertPasswordCommand(string partnerCode, string channelResource, string password)
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