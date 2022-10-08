namespace Bizca.Bff.Infrastructure.Wrappers
{
    using Core.Domain;
    using Core.Infrastructure;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using System;
    using System.Collections;
    using System.Net.Http;
    using System.Threading.Tasks;

    public abstract class BaseWrapper
    {
        private readonly HttpClient httpClient;
        private readonly ILogger logger;

        protected BaseWrapper(ILogger logger, IHttpClientFactory httpClientFactory, string httpClientName)
        {
            if (string.IsNullOrWhiteSpace(httpClientName))
                throw new ArgumentNullException(nameof(httpClientName));

            httpClient = httpClientFactory.CreateClient(httpClientName);
            this.logger = logger;
        }

        protected virtual string ApiVersion { get; } = "api/v1.0";

        protected virtual async Task<IPublicResponse<T>> SendAsync<T>(HttpMethod httpMethod, string requestUrl,
            object content = null, IDictionary metadata = null)
        {
            if (string.IsNullOrWhiteSpace(requestUrl))
                throw new ArgumentNullException(nameof(requestUrl));

            using (var request = new HttpRequestMessage(httpMethod, new Uri(requestUrl, UriKind.Relative)))
            {
                request.AddHeaders(metadata);
                if (httpMethod == HttpMethod.Post || httpMethod == HttpMethod.Patch || httpMethod == HttpMethod.Put)
                    if (content != null)
                    {
                        request.Content = content.GetHttpContent();
                        string requestLog = await request.Content.ReadAsStringAsync();
                        logger.LogDebug($"[Request]= {requestLog}");
                    }

                using (HttpResponseMessage response = await httpClient.SendAsync(request).ConfigureAwait(false))
                {
                    return GetResponseAndLog<T>(response);
                }
            }
        }

        private IPublicResponse<T> GetResponseAndLog<T>(HttpResponseMessage httpResponseMessage)
        {
            string responseAsString = httpResponseMessage.Content.ReadAsStringAsync().Result;
            if (!httpResponseMessage.IsSuccessStatusCode)
                return new PublicResponse<T>(responseAsString, (int)httpResponseMessage.StatusCode);

            logger.LogDebug($"[Response]= {responseAsString}");
            return new PublicResponse<T>(null, (int)httpResponseMessage.StatusCode)
            {
                Data = JsonConvert.DeserializeObject<T>(responseAsString)
            };
        }
    }
}