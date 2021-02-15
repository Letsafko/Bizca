namespace Bizca.User.WebApi.Modules.HealthChecks
{
    using Bizca.Core.Api;
    using Bizca.Core.Api.Modules.HealthChecks;
    using Bizca.Core.Infrastructure.Database.Configuration;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Options;

    /// <summary>
    ///     Bizca database health check.
    /// </summary>
    public class HealthCheckBizcaDb : HealthCheckDatabase
    {
        /// <summary>
        ///     Creates an instance of <see cref="HealthCheckBizcaDb"/>
        /// </summary>
        /// <param name="environment">host environment</param>
        /// <param name="configuration">configuration.</param>
        public HealthCheckBizcaDb(IWebHostEnvironment environment, IOptions<DatabaseConfiguration> configuration)
            : base(environment,
                  configuration?.Value.ConnectionString ?? throw new MissingConfigurationException($"{nameof(HealthCheckBizcaDb)} missing connectionString."),
                  configuration?.Value.UseAzureIdentity ?? throw new MissingConfigurationException($"{nameof(HealthCheckBizcaDb)} missing useAzureIdentity."))
        {
        }
    }
}