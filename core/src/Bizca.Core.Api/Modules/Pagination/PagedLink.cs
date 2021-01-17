namespace Bizca.Core.Api.Modules.Pagination
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