namespace Bizca.User.Application.UseCases.RegisterCodeConfirmation
{
    using Bizca.Core.Application.Abstracts.Commands;
    using Bizca.Core.Domain;
    using Bizca.User.Domain.Agregates.Users;

    public sealed class RegisterCodeConfirmationCommand : ICommand
    {
        public string PartnerCode { get; set; }
        public string ExternalUserId { get; set; }
        public NotificationChanels ChannelId { get; set; }
        public Notification ModelState { get; } = new Notification();
    }
}
