namespace Bizca.Bff.Infrastructure.Wrappers.Notifications.Configurations
{
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class AuthorisationDelegateHandler : DelegatingHandler
    {
        private const string apiKey = "api-key";

        public AuthorisationDelegateHandler(string privateSecretKey)
        {
            PrivateSecretKey = privateSecretKey;
        }

        private string PrivateSecretKey { get; }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            request.Headers.Add(apiKey, PrivateSecretKey);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}