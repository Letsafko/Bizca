namespace Bizca.Core.Api.Modules.HealthChecks
{
    using global::HealthChecks.UI.Client;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics.HealthChecks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Diagnostics.HealthChecks;
    using System;

    public static class HealtCheckhExtensions
    {
        private const int EvaluationTimeInSeconds = 60 * 10;
        private const int MaximunHistoryEntries = 10;
        private static bool AppHealthy = true;
        public static IHealthChecksBuilder AddHealthCheckServices(this IServiceCollection services)
        {
            return services.AddHealthChecksUI(setup =>
            {
                setup.MaximumHistoryEntriesPerEndpoint(MaximunHistoryEntries);
                setup.SetEvaluationTimeInSeconds(EvaluationTimeInSeconds);
            })
            .AddInMemoryStorage()
            .Services
            .AddHealthChecks()
            .AddCheck("self", () => AppHealthy ? HealthCheckResult.Healthy() : HealthCheckResult.Unhealthy());
        }

        public static IApplicationBuilder UseHealthChecks(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
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
                    opt.UIPath = "/health-ui";
                    opt.UseRelativeApiPath = true;
                });
            });

            app.Map("/switch", appBuilder =>
            {
                appBuilder.Run(async context =>
                {
                    AppHealthy = !AppHealthy;
                    await context.Response.WriteAsync($"{Environment.MachineName} health status changed to {AppHealthy}").ConfigureAwait(false);
                });
            });

            return app;
        }
    }
}