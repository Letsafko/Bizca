namespace Bizca.Bff.Infrastructure.Wrappers.Notifications
{
    using Core.Domain;
    using Domain.Wrappers.Notification;
    using Domain.Wrappers.Notification.Requests.Email;
    using Domain.Wrappers.Notification.Requests.Sms;
    using Domain.Wrappers.Notification.Responses;
    using Microsoft.Extensions.Logging;
    using System.Collections;
    using System.Net.Http;
    using System.Threading.Tasks;

    public sealed class NotificationWrapper : BaseWrapper, INotificationWrapper
    {
        public NotificationWrapper(IHttpClientFactory httpClientFactory, 
            ILogger<NotificationWrapper> logger)
            : base(logger, httpClientFactory, NamedHttpClients.ApiNotificationClientName)
        {
        }

        protected override string ApiVersion => "v3";

        public async Task<IPublicResponse<TransactionalEmailResponse>> SendTransactionalEmail(
            TransactionalEmailRequest request,
            IDictionary headers = null)
        {
            return await SendAsync<TransactionalEmailResponse>(HttpMethod.Post,
                $"{ApiVersion}/smtp/email",
                request,
                headers);
        }

        public async Task<IPublicResponse<TransactionalSmsResponse>> SendTransactionalSms(
            TransactionalSmsRequest request,
            IDictionary headers = null)
        {
            return await SendAsync<TransactionalSmsResponse>(HttpMethod.Post,
                $"{ApiVersion}/transactionalSMS/sms",
                request,
                headers);
        }
    }
}