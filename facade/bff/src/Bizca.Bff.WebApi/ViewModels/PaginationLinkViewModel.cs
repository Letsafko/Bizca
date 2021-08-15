namespace Bizca.Bff.WebApi.ViewModels
{
    /// <summary>
    ///     Pagination link view model.
    /// </summary>
    public sealed class PaginationLinkViewModel
    {
        /// <summary>
        ///     Creates an instance of <see cref="PaginationLinkViewModel"/>
        /// </summary>
        /// <param name="url">link to forecast page</param>
        /// <param name="relation">direction to forecast page</param>
        public PaginationLinkViewModel(string url, string relation)
        {
            Url = url;
            Relation = relation;
        }

        /// <summary>
        ///     Link to next or previous page.
        /// </summary>
        public string Url { get; }
        
        /// <summary>
        ///     Direction of forecast page.
        /// </summary>
        public string Relation { get; }
    }
}
