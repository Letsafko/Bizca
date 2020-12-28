namespace Bizca.Core.Application.Abstracts.Paging
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class Pagination<T> where T : class
    {
        private const string next = "next";
        private const string previous = "previous";
        private const string queryStringSeparator = "?";
        private const string queryStringParameterSeparator = "&";

        private int pageSize { get; }
        private int rowcount { get; }
        private string requestPath { get; }
        private IEnumerable<T> results { get; }
        public Pagination(int pageSize, string requestPath, List<T> results)
        {
            this.pageSize = pageSize;
            rowcount = results.Count;
            this.requestPath = requestPath;
            this.results = results.Take(pageSize);

            FirstIndex = rowcount == 0 ? default : results[0];
            LastIndex = rowcount == 0 ? default : pageSize > rowcount ? results[rowcount - 1] : results[rowcount - 2];
        }

        public T LastIndex { get; }
        public T FirstIndex { get; }

        public PagedResult<T> GetPaged<TRequest>(TRequest request, int firstIndex, int lastIndex)
            where TRequest : Paged, ICloneable
        {
            if(rowcount < 1)
            {
                return new PagedResult<T>(results, default);
            }

            var relations = new List<PagedLink>();
            if (pageSize >= rowcount)
            {
                relations.Add(request.PageNumber > 1
                    ? GetPagedLink(request.Clone() as TRequest, previous, firstIndex, lastIndex)
                    : GetPagedLink(request.Clone() as TRequest, next, 0, 0));
            }
            else
            {
                if (request.PageNumber > 1)
                    relations.Add(GetPagedLink(request.Clone() as TRequest, previous, firstIndex, lastIndex));

                relations.Add(GetPagedLink(request.Clone() as TRequest, next, firstIndex, lastIndex));
            }

            return new PagedResult<T>(results.Take(request.PageSize), relations);
        }

        #region private

        private string GetQueryString(Paged request)
        {
            var jsonSerializerSettings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            string jsonquery = JsonConvert.SerializeObject(request, Formatting.None, jsonSerializerSettings);
            IEnumerable<string> parameters = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonquery)
                                                        .Select(p => $"{p.Key.ToLower()}={p.Value}");

            return $"{requestPath}{queryStringSeparator}{string.Join(queryStringParameterSeparator, parameters)}";
        }

        private PagedLink GetPagedLink(Paged request, string direction, int firstIndex, int lastIndex)
        {
            if (direction == next)
            {
                request.PageNumber = lastIndex == 0 ? 1 : ++request.PageNumber;
                request.PageIndex = lastIndex;
                request.Direction = direction;
            }
            else if (request.PageNumber <= 2)
            {
                request.PageNumber = 1;
                request.PageIndex = 0;
                request.Direction = next;
            }
            else
            {
                request.PageNumber--;
                request.PageIndex = firstIndex;
                request.Direction = direction;
            }
            return new PagedLink(GetQueryString(request), direction);
        }

        #endregion
    }
}
