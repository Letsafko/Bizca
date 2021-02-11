namespace Bizca.Core.Api.Modules.Configuration
{
    using Microsoft.ApplicationInsights.AspNetCore.Extensions;

    public class ApplicationInsightsConfigurationModel : ApplicationInsightsServiceOptions
    {
        /// <summary>Gets or sets the name of the system.</summary>
        /// <value>The name of the system.</value>
        public string SystemName { get; set; }
    }
}