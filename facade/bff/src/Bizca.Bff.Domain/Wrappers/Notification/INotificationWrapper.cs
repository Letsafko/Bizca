namespace Bizca.Bff.Domain.Wrappers.Notification
{
    using Bizca.Bff.Domain.Wrappers.Notification.Requests.Email;
    using Bizca.Bff.Domain.Wrappers.Notification.Requests.Sms;
    using Bizca.Bff.Domain.Wrappers.Notification.Responses;
    using Bizca.Core.Domain;
    using System.Collections;
    using System.Threading.Tasks;
    public interface INotificationWrapper
    {
        Task<IPublicResponse<TransactionalEmailResponse>> SendEmail(TransactionalEmailRequest request,
            IDictionary headers = null);

        Task<IPublicResponse<TransactionalSmsResponse>> SendSms(TransactionalSmsRequest request,
            IDictionary headers = null);
    }
}