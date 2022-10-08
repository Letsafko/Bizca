namespace Bizca.Core.Api.Modules.Configuration
{
    using Microsoft.ApplicationInsights.AspNetCore.Extensions;

    public class ApplicationInsightsConfigurationModel : ApplicationInsightsServiceOptions
    {
        /// <summary>
        ///     Gets or sets the application system name.
        /// </summary>
        public string ApplicationName { get; set; }
    }
}