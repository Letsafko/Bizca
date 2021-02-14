namespace Bizca.Core.Api.HealthChecks
{
    using global::HealthChecks.UI.Client;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics.HealthChecks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Diagnostics.HealthChecks;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public static class HealthExtensions
    {
        private static bool AppHealthy = true;
        public static IHealthChecksBuilder AddHealthCheckServices(this IServiceCollection services)
        {
            return services.AddHealthChecksUI()
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

        private static Task WriteHealthCheckUIResponse(HttpContext context, HealthReport result)
        {
            context.Response.ContentType = "application/json";

            var json = new JObject(
                new JProperty("status", result.Status.ToString()),
                new JProperty("results", new JObject(result.Entries.Select(pair =>
                    new JProperty(pair.Key, new JObject(
                        new JProperty("status", pair.Value.Status.ToString()),
                        new JProperty("description", pair.Value.Description)))))));

            return context.Response.WriteAsync(
                json.ToString(Formatting.Indented));
        }
    }
}
