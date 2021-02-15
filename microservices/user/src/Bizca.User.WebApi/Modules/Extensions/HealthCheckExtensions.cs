namespace Bizca.User.WebApi.Modules.Extensions
{
    using Bizca.Core.Api.Modules.HealthChecks;
    using Bizca.User.WebApi.Modules.HealthChecks;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    ///     Helath checks extensions.
    /// </summary>
    public static class HealthCheckExtensions
    {
        /// <summary>
        ///     Configures health checks.
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection ConfigureHealthChecks(this IServiceCollection services)
        {
            services.AddHealthCheckServices()
                    .AddCheck<HealthCheckBizcaDb>("bizcaDb", tags: new[] { "database" });

            return services;
        }
    }
}
