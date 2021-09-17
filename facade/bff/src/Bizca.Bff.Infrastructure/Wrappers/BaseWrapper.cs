namespace Bizca.Bff.Infrastructure.Wrappers
{
    using Bizca.Core.Domain;
    using Bizca.Core.Infrastructure;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using System;
    using System.Collections;
    using System.Net.Http;
    using System.Threading.Tasks;
    public class BaseWrapper
    {
        private readonly ILogger logger;
        private readonly HttpClient httpClient;
        internal BaseWrapper(ILogger logger, IHttpClientFactory httpClientFactory, string httpClientName)
        {
            if (string.IsNullOrWhiteSpace(httpClientName))
                throw new ArgumentNullException(nameof(httpClientName));

            httpClient = httpClientFactory.CreateClient(httpClientName);
            this.logger = logger;
        }

        protected virtual string ApiVersion { get; } = "api/v1.0";
        internal async Task<IPublicResponse<T>> SendAsync<T>(HttpMethod httpMethod, string requestUrl, object content = null, IDictionary metadata = null)
        {
            if (string.IsNullOrWhiteSpace(requestUrl))
                throw new ArgumentNullException(nameof(requestUrl));

            using (var request = new HttpRequestMessage(httpMethod, new Uri(requestUrl, UriKind.Relative)))
            {
                request.AddHeaders(metadata);
                if (httpMethod == HttpMethod.Post || httpMethod == HttpMethod.Patch || httpMethod == HttpMethod.Put)
                {

                    if (content != null)
                    {
                        request.Content = content.GetHttpContent();
                        string requestLog = await request.Content.ReadAsStringAsync();
                        logger.LogDebug($"[Request]= {requestLog}");
                    }
                }

                using (HttpResponseMessage response = await httpClient.SendAsync(request).ConfigureAwait(false))
                {
                    return GetResponseAndLog<T>(response);
                }
            }
        }

        #region private helpers

        private IPublicResponse<T> GetResponseAndLog<T>(HttpResponseMessage httpResponseMessage)
        {
            string result = httpResponseMessage.Content.ReadAsStringAsync().Result;
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                return new PublicResponse<T>(result, (int)httpResponseMessage.StatusCode);
            }

            logger.LogDebug($"[Response]= {result}");
            return new PublicResponse<T>(null, (int)httpResponseMessage.StatusCode)
            {
                Data = JsonConvert.DeserializeObject<T>(result)
            };
        }
        public class ErrorResponse : IPublicResponse
        {
            public bool Success { get; set; }
            public object Message { get; set; }
            public int? ErrorCode { get; set; }
            public int StatusCode { get; set; }
        }

        #endregion
    }
}