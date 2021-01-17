namespace Bizca.Core.Api.Modules.Pagination
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public sealed class PagedResult<T> where T : class
    {
        public PagedResult(IEnumerable<T> value, IEnumerable<PagedLink> relations)
        {
            Relations = relations;
            Value = value ?? new List<T>();
        }
        public IEnumerable<T> Value { get; }

        [JsonProperty("relations", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<PagedLink> Relations { get; }
    }
}