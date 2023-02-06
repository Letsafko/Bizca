namespace Bizca.Core.Domain.Cqrs.Events
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IProcessNotification
    {
        Task ProcessNotificationAsync(INotificationEvent notificationEvent, CancellationToken cancellationToken);
    }
}