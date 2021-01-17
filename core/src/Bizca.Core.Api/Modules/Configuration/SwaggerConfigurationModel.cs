namespace Bizca.Core.Api.Modules.Configuration
{
    using System.Collections.Generic;

    public sealed class SwaggerConfigurationModel
    {
        /// <summary>
        ///     Gets or sets versions configurations.
        /// </summary>
        public IEnumerable<VersionConfigurationModel> Versions { get; set; }

        /// <summary>
        ///     Gets or sets api xml documentation configurations.
        /// </summary>
        public IEnumerable<string> XmlDocumentations { get; set; }

        public IEnumerable<SwaggerSecurityDefinitionModel> Security { get; set; }

        /// <summary>
        ///     Gets or sets sts security configuration.
        /// </summary>
        public SwaggerStsSecurityModel StsSecurity { get; set; }
    }
}