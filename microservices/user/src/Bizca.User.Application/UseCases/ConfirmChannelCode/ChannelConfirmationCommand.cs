namespace Bizca.User.Application.UseCases.ConfirmChannelCode
{
    using Core.Domain.Cqrs.Commands;
    using Domain;

    public sealed class ChannelConfirmationCommand : ICommand
    {
        public string PartnerCode { get; set; }
        public string ExternalUserId { get; set; }
        public string CodeConfirmation { get; set; }
        public ChannelType ChannelType { get; set; }
    }
}