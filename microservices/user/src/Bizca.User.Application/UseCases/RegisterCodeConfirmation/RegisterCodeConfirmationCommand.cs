namespace Bizca.User.Application.UseCases.RegisterCodeConfirmation
{
    using Bizca.Core.Application.Commands;
    using Bizca.User.Domain;

    public sealed class RegisterCodeConfirmationCommand : ICommand
    {
        public string PartnerCode { get; set; }
        public string ExternalUserId { get; set; }
        public ChannelType ChannelType { get; set; }
    }
}