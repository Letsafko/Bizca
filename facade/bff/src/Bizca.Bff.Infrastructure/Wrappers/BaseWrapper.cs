namespace Bizca.Bff.Infrastructure.Wrappers
{
    using Core.Domain;
    using Core.Infrastructure;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using System;
    using System.Collections;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    public abstract class BaseWrapper
    {
        private readonly ILogger<BaseWrapper> _logger;
        private readonly HttpClient _httpClient;

        protected BaseWrapper(ILogger<BaseWrapper> logger, 
            IHttpClientFactory httpClientFactory, 
            string httpClientName)
        {
            if (string.IsNullOrWhiteSpace(httpClientName))
                throw new ArgumentNullException(nameof(httpClientName));

            _httpClient = httpClientFactory.CreateClient(httpClientName);
            _logger = logger;
        }

        protected virtual string ApiVersion => "api/v1.0";

        protected async Task<IPublicResponse<T>> SendAsync<T>(HttpMethod httpMethod, 
            string relativeRequestUrl,
            object content = null, 
            IDictionary metadata = null)
        {
            if (string.IsNullOrWhiteSpace(relativeRequestUrl))
                throw new ArgumentNullException(nameof(relativeRequestUrl));

            using var request = new HttpRequestMessage(httpMethod, new Uri(relativeRequestUrl, UriKind.Relative));
            request.AddHeaders(metadata);
            if (httpMethod == HttpMethod.Post || httpMethod == HttpMethod.Patch || httpMethod == HttpMethod.Put)
                if (content != null)
                {
                    request.Content = content.GetHttpContent();
                    string requestLog = await request.Content!.ReadAsStringAsync();
                    _logger.LogDebug("[Request]= {Request}", requestLog);
                }

            using var response = await _httpClient.SendAsync(request);
            return await GetResponseAndLogAsync<T>(response);
        }

        private async Task<IPublicResponse<T>> GetResponseAndLogAsync<T>(HttpResponseMessage httpResponseMessage)
        {
            var responseAsString = await httpResponseMessage.Content.ReadAsStringAsync();
            var statusCode = httpResponseMessage.StatusCode;
        
            _logger.LogDebug("[StatusCode]={StatusCode}, [Response]= {Response}", 
                statusCode.ToString(), 
                responseAsString);
        
            if (!httpResponseMessage.IsSuccessStatusCode)
                return new PublicResponse<T>(default, 
                    (int)statusCode,
                    httpResponseMessage.ReasonPhrase);

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<T>();
            return new PublicResponse<T>(response, (int)statusCode);
        }
    }
}