namespace Bizca.Bff.Infrastructure.Wrappers.Notifications
{
    using Bizca.Bff.Domain.Wrappers.Notification;
    using Bizca.Bff.Domain.Wrappers.Notification.Requests;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Mail;
    using System.Threading.Tasks;

    public sealed class NotificationWrapper : BaseWrapper, INotificationWrapper
    {
        private readonly SmtpSettings smtpSettings;
        public NotificationWrapper(IOptions<SmtpSettings> smtpOptions, IHttpClientFactory httpClientFactory, ILogger<NotificationWrapper> logger) 
            : base(logger, httpClientFactory, NamedHttpClients.ApiNotificationClientName)
        {
            smtpSettings = smtpOptions?.Value ?? throw new ArgumentNullException(nameof(smtpOptions));
        }

        public async Task SendConfirmationEmail(TransactionalEmailRequest request)
        {
            using (var smtpClient = new SmtpClient(smtpSettings.Host, smtpSettings.Port))
            {
                smtpClient.Credentials = new NetworkCredential(smtpSettings.UserName, smtpSettings.Password);
                smtpClient.EnableSsl = true;

                var mailMessage = new MailMessage(request.From, request.To, request.Subject, request.Body);
                await smtpClient.SendMailAsync(mailMessage);
            }
        }
    }
}
