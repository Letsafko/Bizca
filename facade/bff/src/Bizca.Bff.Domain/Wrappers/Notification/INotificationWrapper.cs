namespace Bizca.Bff.Domain.Wrappers.Notification
{
    using Bizca.Bff.Domain.Wrappers.Notification.Requests;
    using Bizca.Bff.Domain.Wrappers.Notification.Responses;
    using System.Collections;
    using System.Threading.Tasks;
    public interface INotificationWrapper
    {
        Task<TransactionalEmailResponse> SendEmail(TransactionalEmailRequest request, IDictionary headers = null);
    }
}