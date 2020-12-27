namespace Bizca.Core.Api.Modules.Configuration
{
    public sealed class VersioningConfigurationModel
    {
        /// <summary>
        ///     Gets or sets default api version.
        /// </summary>
        public string Default { get; set; }

        /// <summary>
        ///     Gets or sets route constraint.
        /// </summary>
        public string RouteConstraintName { get; set; }
    }
}
