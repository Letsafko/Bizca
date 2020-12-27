namespace Bizca.Core.Api.Modules.Configuration
{
    using System.Collections.Generic;
    public sealed class SwaggerStsSecurityModel
    {
        /// <summary>
        ///     Gets or sets client Id.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        ///     Gets or sets client secret.
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        ///     Gets or sets sts url.
        /// </summary>
        public string StsUrl { get; set; }

        /// <summary>
        ///     Gets or sets scopes.
        /// </summary>
        public IEnumerable<string> Scopes { get; set; }
    }
}
