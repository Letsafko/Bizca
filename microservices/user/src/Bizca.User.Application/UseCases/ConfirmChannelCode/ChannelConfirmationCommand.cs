namespace Bizca.User.Application.UseCases.ConfirmChannelCode
{
    using Bizca.Core.Application.Commands;
    using Bizca.User.Domain.ValueObjects;

    public sealed class ChannelConfirmationCommand : ICommand
    {
        public string PartnerCode { get; set; }
        public string ExternalUserId { get; set; }
        public string CodeConfirmation { get; set; }
        public ChannelType ChannelType { get; set; }
    }
}