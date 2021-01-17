namespace Bizca.Core.Application.Events
{
    using System.Threading.Tasks;

    public interface IProcessNotification
    {
        Task ProcessNotificationAsync(IEvent @event);
    }
}