namespace Bizca.Core.Api.Modules.Configuration
{
    using System;

    public sealed class ConsulConfigurationModel
    {
        public ConsulConfigurationModel()
        {
            HealthCheckEndPoint = "health";
        }

        /// <summary>
        ///     Gets or sets host.
        /// </summary>
        public Uri ConsulHost { get; set; }

        /// <summary>
        ///     Gets or sets token.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        ///     Gets or sets service name.
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        ///     Gets or sets health check endpoint.
        /// </summary>
        public string HealthCheckEndPoint { get; set; }

        /// <summary>
        ///     Gets or sets system address.
        /// </summary>
        public Uri SystemAddress { get; set; }
    }
}