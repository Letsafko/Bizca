namespace Bizca.Bff.Infrastructure.Wrappers.Notifications
{
    using Bizca.Bff.Domain.Wrappers.Notification;
    using Bizca.Bff.Domain.Wrappers.Notification.Requests.Email;
    using Bizca.Bff.Domain.Wrappers.Notification.Requests.Sms;
    using Bizca.Bff.Domain.Wrappers.Notification.Responses;
    using Bizca.Core.Domain;
    using Microsoft.Extensions.Logging;
    using System.Collections;
    using System.Net.Http;
    using System.Threading.Tasks;

    public sealed class NotificationWrapper : BaseWrapper, INotificationWrapper
    {
        public NotificationWrapper(IHttpClientFactory httpClientFactory, ILogger<NotificationWrapper> logger)
            : base(logger, httpClientFactory, NamedHttpClients.ApiNotificationClientName)
        {
        }

        protected override string ApiVersion { get; } = "v3";
        public async Task<IPublicResponse<TransactionalEmailResponse>> SendEmail(TransactionalEmailRequest request,
            IDictionary headers = null)
        {
            return await SendAsync<TransactionalEmailResponse>(HttpMethod.Post,
                $"{ApiVersion}/smtp/email",
                request,
                headers);
        }

        public async Task<IPublicResponse<TransactionalSmsResponse>> SendSms(TransactionalSmsRequest request,
            IDictionary headers = null)
        {
            return await SendAsync<TransactionalSmsResponse>(HttpMethod.Post,
                $"{ApiVersion}/transactionalSMS/sms",
                request,
                headers);
        }
    }
}