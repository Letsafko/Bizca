namespace Bizca.Bff.Domain.Wrappers.Notification
{
    using Bizca.Bff.Domain.Wrappers.Notification.Requests;
    using System.Threading.Tasks;
    public interface INotificationWrapper
    {
        Task SendConfirmationEmail(TransactionalEmailRequest request);
    }
}