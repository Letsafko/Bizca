namespace Bizca.Core.Application.Abstracts.Paging
{
    public class Paged
    {
        /// <summary>
        ///     Gets or sets page index.
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        ///     Gets or sets page size.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        ///     Gets or sets page number.
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        ///     Gets or sets request direction(next or previous).
        /// </summary>
        public string Direction { get; set; }
    }
}
