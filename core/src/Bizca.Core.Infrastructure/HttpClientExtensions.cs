namespace Bizca.Core.Infrastructure
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using System.Collections;
    using System.Net.Http;
    using System.Text;
    public static class HttpClientExtensions
    {
        public const string JsonMediaType = "application/json";
        private static readonly JsonSerializerSettings jsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore
        };

        public static HttpContent GetHttpContent(this object content)
        {
            string json = JsonConvert.SerializeObject(content, jsonSettings);
            return new StringContent(json, Encoding.UTF8, JsonMediaType);
        }

        public static void AddHeaders(this HttpRequestMessage request, IDictionary headers)
        {
            if (headers != null)
            {
                foreach (object entry in headers.Keys)
                {
                    request.Headers.Add(entry.ToString(), headers[entry].ToString());
                }
            }
        }
    }
}