namespace Bizca.User.WebApi.Modules.HealthChecks
{
    using Bizca.Core.Api.HealthChecks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;

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
        public HealthCheckBizcaDb(IWebHostEnvironment environment, IConfiguration configuration)
            : base(environment, configuration.GetValue<string>("BizcaDatabase:ConnectionString"))
        {
        }
    }
}