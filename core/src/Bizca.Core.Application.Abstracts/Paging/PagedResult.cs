namespace Bizca.Core.Application.Abstracts.Paging
{
    using System.Collections.Generic;

    public sealed class PagedResult<T> where T : class
    {
        public PagedResult(IEnumerable<T> value, IEnumerable<PagedLink> relations)
        {
            Value = value;
            Relations = relations;
        }
        public IEnumerable<T> Value { get; }
        public IEnumerable<PagedLink> Relations { get; }
    }
}
