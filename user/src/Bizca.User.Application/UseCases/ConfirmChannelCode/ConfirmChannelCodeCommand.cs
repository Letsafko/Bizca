namespace Bizca.User.Application.UseCases.ConfirmChannelCode
{
    using Bizca.Core.Application.Abstracts.Commands;
    using Bizca.Core.Domain;
    using Bizca.User.Domain.Agregates.Users;

    public sealed class ConfirmChannelCodeCommand : ICommand
    {
        public string PartnerCode { get; set; }
        public string ExternalUserId { get; set; }
        public string CodeConfirmation { get; set; }
        public NotificationChanels ChannelId { get; set; }
        public Notification ModelState { get; } = new Notification();
    }
}
