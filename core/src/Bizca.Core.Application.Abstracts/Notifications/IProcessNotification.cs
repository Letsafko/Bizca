namespace Bizca.Core.Application.Abstracts.Notifications
{
    using MediatR;
    using System.Threading.Tasks;

    public interface IProcessNotification
    {
        Task ProcessNotificationAsync(INotification notification);
    }
}
