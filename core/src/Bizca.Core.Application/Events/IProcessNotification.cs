namespace Bizca.Core.Application.Events
{
    using Bizca.Core.Domain;
    using System.Threading.Tasks;
    public interface IProcessNotification
    {
        Task ProcessNotificationAsync(IEvent @event);
    }
}