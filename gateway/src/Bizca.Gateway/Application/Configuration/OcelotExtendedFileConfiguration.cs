namespace Bizca.Gateway.Application.Configuration
{
    using MMLib.SwaggerForOcelot.Configuration;
    using Ocelot.Configuration.File;
    using System.Collections.Generic;

    /// <summary>Extended ocelot file configuration</summary>
    public class OcelotExtendedFileConfiguration : FileConfiguration
    {
        /// <summary>Initializes a new instance of the <see cref="OcelotExtendedFileConfiguration" /> class.</summary>
        public OcelotExtendedFileConfiguration()
        {
            Routes = new List<ExtendedFileRoute>();
            SwaggerEndPoints = new List<SwaggerEndPointOptions>();
        }

        /// <summary>Gets or sets the routes.</summary>
        /// <value>The routes.</value>
        public new List<ExtendedFileRoute> Routes { get; set; }

        /// <summary>Gets or sets the swagger end points.</summary>
        /// <value>The swagger end points.</value>
        public List<SwaggerEndPointOptions> SwaggerEndPoints { get; set; }
    }
}