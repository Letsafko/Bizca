namespace Bizca.Bff.Domain.Wrappers.Notification
{
    using Core.Domain;
    using Requests.Email;
    using Requests.Sms;
    using Responses;
    using System.Collections;
    using System.Threading.Tasks;

    public interface INotificationWrapper
    {
        Task<IPublicResponse<TransactionalEmailResponse>> SendTransactionalEmail(TransactionalEmailRequest request,
            IDictionary headers = null);

        Task<IPublicResponse<TransactionalSmsResponse>> SendTransactionalSms(TransactionalSmsRequest request,
            IDictionary headers = null);
    }
}