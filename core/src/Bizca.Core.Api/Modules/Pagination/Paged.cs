namespace Bizca.Core.Api.Modules.Pagination
{
    public class Paged
    {
        /// <summary>
        ///     Gets or sets page index.
        /// </summary>
        public int PageIndex { get; set; } = 0;

        /// <summary>
        ///     Gets or sets page size.
        /// </summary>
        public int PageSize { get; set; } = 20;

        /// <summary>
        ///     Gets or sets request direction(next or previous).
        /// </summary>
        public string Direction { get; set; } = "next";
    }
}