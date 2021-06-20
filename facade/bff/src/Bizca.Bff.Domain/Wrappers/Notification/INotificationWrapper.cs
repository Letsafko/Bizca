namespace Bizca.Bff.Domain.Wrappers.Notification
{
    using System.Threading.Tasks;
    public interface INotificationWrapper
    {
        Task SendConfirmationEmail();
    }
}
