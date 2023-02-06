namespace Bizca.Bff.WebApi.Modules.Extensions
{
    using Core.Api.Modules.HealthChecks;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    ///     Health checks extensions.
    /// </summary>
    public static class HealthCheckExtensions
    {
        /// <summary>
        ///     Configures health checks.
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection ConfigureHealthChecks(this IServiceCollection services)
        {
            services.AddHealthCheckServices();
            return services;
        }
    }
}