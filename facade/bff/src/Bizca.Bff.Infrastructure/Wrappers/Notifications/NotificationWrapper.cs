namespace Bizca.Bff.Infrastructure.Wrappers.Notifications
{
    using Bizca.Bff.Domain.Wrappers.Notification;
    using Bizca.Bff.Domain.Wrappers.Notification.Requests;
    using Bizca.Bff.Domain.Wrappers.Notification.Responses;
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
        public async Task<TransactionalEmailResponse> SendConfirmationEmail(TransactionalEmailRequest request, IDictionary headers = null)
        {
            return await SendAsync<TransactionalEmailResponse>(HttpMethod.Post,
                $"{ApiVersion}/smtp/email",
                request,
                headers);
        }
    }
}