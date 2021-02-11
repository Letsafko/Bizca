namespace Bizca.Gateway.Application.Configuration
{
    using Ocelot.Configuration.File;

    /// <summary>Extended Ocelot file route</summary>
    public class ExtendedFileRoute : FileRoute
    {
        /// <summary>Gets or sets the swagger key.</summary>
        /// <value>The swagger key.</value>
        public string SwaggerKey { get; set; }
    }
}