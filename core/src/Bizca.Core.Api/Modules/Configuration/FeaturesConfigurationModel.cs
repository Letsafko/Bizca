namespace Bizca.Core.Api.Modules.Configuration
{
    public class FeaturesConfigurationModel
    {
        /// <summary>
        ///     Enable or disable logging.
        /// </summary>
        public bool Logging { get; set; }

        /// <summary>
        ///     Enable or disable api versioning.
        /// </summary>
        public bool Versioning { get; set; }

        /// <summary>
        ///     Enable or disable swagger.
        /// </summary>
        public bool Swagger { get; set; }

        /// <summary>
        ///     Enable or disable insights.
        /// </summary>
        public bool ApplicationInsights { get; set; }

        /// <summary>
        ///     Enable or disable consul.
        /// </summary>
        public bool Consul { get; set; }

        /// <summary>
        ///     Enable or disable cors.
        /// </summary>
        public bool Cors { get; set; }

        /// <summary>
        ///     Enable or disable security authentication.
        /// </summary>
        public bool Sts { get; set; }

        /// <summary>
        ///     Enable or disable key vault.
        /// </summary>
        public bool KeyVault { get; set; }
    }
}