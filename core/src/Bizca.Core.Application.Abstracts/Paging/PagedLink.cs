namespace Bizca.Core.Application.Abstracts.Paging
{
    public sealed class PagedLink
    {
        public PagedLink(string url, string relation)
        {
            Url = url;
            Relation = relation;
        }

        public string Url { get; }
        public string Relation { get; }
    }
}
