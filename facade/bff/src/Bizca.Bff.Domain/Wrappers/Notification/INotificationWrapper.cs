namespace Bizca.Bff.Domain.Wrappers.Notification
{
    using Bizca.Bff.Domain.Wrappers.Notification.Requests;
    using Bizca.Bff.Domain.Wrappers.Notification.Responses;
    using Bizca.Core.Domain;
    using System.Collections;
    using System.Threading.Tasks;
    public interface INotificationWrapper
    {
        Task<IPublicResponse<TransactionalEmailResponse>> SendEmail(TransactionalEmailRequest request, IDictionary headers = null);
    }
}