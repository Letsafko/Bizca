namespace Bizca.Gateway.Application.Extensions
{
    using Configuration;
    using Core.Api.Modules.Configuration;
    using Core.Api.Modules.Extensions;
    using HealthChecks.UI.Client;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics.HealthChecks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Diagnostics.HealthChecks;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>Health checks extension methods</summary>
    public static class HealthExtensions
    {
        /// <summary>Adds the health checks.</summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        [ExcludeFromCodeCoverage]
        public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            FeaturesConfigurationModel features = configuration.GetFeaturesConfiguration();

            IHealthChecksBuilder healthChecks = services.AddHealthChecks();

            if (features.Consul)
                healthChecks.AddConsul(options =>
                {
                    options.HostName = configuration["GlobalConfiguration:ServiceDiscoveryProvider:Host"];
                    options.Port = int.Parse(configuration["GlobalConfiguration:ServiceDiscoveryProvider:Port"]);
                    options.Password = configuration["GlobalConfiguration:ServiceDiscoveryProvider:Token"];
                }, "ConsulCheck", tags: new[] { "consul" });

            var healthEndpoints = configuration.GetSection("Health").Get<List<HealthEndpoint>>();

            foreach (HealthEndpoint healthEndpoint in healthEndpoints)
                if (Uri.TryCreate(healthEndpoint.Endpoint, UriKind.Absolute, out Uri endpointUri))
                    healthChecks.AddUrlGroup(endpointUri,
                        healthEndpoint.Name,
                        HealthStatus.Degraded);

            services.AddHealthChecksUI()
                .AddInMemoryStorage();

            return services;
        }

        /// <summary>Uses the health checks.</summary>
        /// <param name="app">The application.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        [ExcludeFromCodeCoverage]
        public static IApplicationBuilder UseHealthChecks(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health",
                    new HealthCheckOptions
                    {
                        ResultStatusCodes =
                        {
                            [HealthStatus.Healthy] = StatusCodes.Status200OK,
                            [HealthStatus.Degraded] = StatusCodes.Status200OK,
                            [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                        },
                        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                    });

                endpoints.MapHealthChecksUI(opt =>
                {
                    opt.UseRelativeApiPath = true;
                });
            });

            return app;
        }
    }
}