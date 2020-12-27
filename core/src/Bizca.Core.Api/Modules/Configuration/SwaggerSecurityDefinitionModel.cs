namespace Bizca.Core.Api.Modules.Configuration
{
    using System.ComponentModel.DataAnnotations;

    public sealed class SwaggerSecurityDefinitionModel
    {
        public string DisplayName { get; set; }

        /// <summary>
        ///     Gets or sets the name of the header, query or cookie parameter to be used.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets short description security name.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Gets or sets location of api key ('query', 'header' or 'cookie').
        /// </summary>
        [Required]
        public string In { get; set; }

        /// <summary>
        ///     Gets or sets type of security scheme('apiKey', 'http', 'oauth2', 'openIdConnect').
        /// </summary>
        [Required]
        public string Type { get; set; }

        /// <summary>
        ///     Gets or sets security scheme.
        /// </summary>
        [Required]
        public string Scheme { get; set; }
    }
}
