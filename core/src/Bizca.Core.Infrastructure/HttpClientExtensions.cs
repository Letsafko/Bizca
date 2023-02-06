namespace Bizca.Core.Infrastructure
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using System;
    using System.Collections;
    using System.Net.Http;
    using System.Text;

    public static class HttpClientExtensions
    {
        private const string JsonMediaType = "application/json";

        private static readonly JsonSerializerSettings JsonSettings = new()
        {
            ContractResolver = new CamelCaseExceptDictionaryKeysResolver(),
            NullValueHandling = NullValueHandling.Ignore
        };

        public static HttpContent GetHttpContent(this object content)
        {
            string json = JsonConvert.SerializeObject(content, JsonSettings);
            return new StringContent(json, Encoding.UTF8, JsonMediaType);
        }

        public static void AddHeaders(this HttpRequestMessage request, IDictionary headers)
        {
            if (headers is null) return;

            foreach (object entry in headers.Keys)
            {
                if (entry != null)
                {
                    request.Headers.Add(entry.ToString()!, headers[entry].ToString());
                }
            }
        }
    }

    internal class CamelCaseExceptDictionaryKeysResolver : CamelCasePropertyNamesContractResolver
    {
        protected override JsonDictionaryContract CreateDictionaryContract(Type objectType)
        {
            JsonDictionaryContract contract = base.CreateDictionaryContract(objectType);
            contract.DictionaryKeyResolver = propertyName => propertyName;
            return contract;
        }
    }
}