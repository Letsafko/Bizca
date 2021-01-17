namespace Bizca.Core.Api.Modules.Configuration
{
    public sealed class VersionConfigurationModel
    {
        /// <summary>
        ///     Gets or sets api version.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        ///     Gets or sets api title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Gets or sets api description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Gets or sets email author.
        /// </summary>
        public string Email { get; set; }
    }
}