namespace Bizca.Bff.Infrastructure.Wrappers.Notifications.Configurations
{
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    public sealed class AuthorisationDelegateHandler : DelegatingHandler
    {
        private string PrivateSecretKey { get; }
        public AuthorisationDelegateHandler(string privateSecretKey)
        {
            PrivateSecretKey = privateSecretKey;
        }
        private const string apiKey = "api-key";

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add(apiKey, PrivateSecretKey);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}