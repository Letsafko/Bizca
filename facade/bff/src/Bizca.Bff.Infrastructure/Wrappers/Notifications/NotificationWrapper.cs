namespace Bizca.Bff.Infrastructure.Wrappers.Notifications
{
    using Bizca.Bff.Domain.Wrappers.Notification;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    public sealed class NotificationWrapper : BaseWrapper, INotificationWrapper
    {
        public NotificationWrapper(IHttpClientFactory httpClientFactory, ILogger<NotificationWrapper> logger) 
            : base(logger, httpClientFactory, NamedHttpClients.ApiNotificationClientName)
        {
        }

        public async Task SendConfirmationEmail()
        {
            throw new NotImplementedException();
        }
    }
}
