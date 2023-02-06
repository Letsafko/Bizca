namespace Bizca.Core.Api.Modules.HealthChecks
{
    using global::HealthChecks.UI.Client;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics.HealthChecks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Diagnostics.HealthChecks;
    using System;

    public static class HealthCheckExtensions
    {
        private const int MinimumSecondsBetweenFailure = 1 * 60;
        private const int EvaluationTimeInSeconds = 60 * 10;
        private const int MaximumHistoryEntries = 10;

        private const string DatabaseTagName = "database";

        public static void AddHealthCheckServices(this IServiceCollection services)
        {
            services.AddHealthChecksUI(setup =>
                {
                    setup.SetMinimumSecondsBetweenFailureNotifications(MinimumSecondsBetweenFailure)
                        .MaximumHistoryEntriesPerEndpoint(MaximumHistoryEntries)
                        .SetEvaluationTimeInSeconds(EvaluationTimeInSeconds);
                })
                .AddInMemoryStorage()
                .Services
                .AddHealthChecks()
                .AddCheck("self", _ => HealthCheckResult.Healthy(), tags: new[] { "self" })
                .AddCheck<HealthCheckDatabase>(DatabaseTagName, tags: new[] { "database" });
        }

        public static void UseHealthChecks(this IApplicationBuilder app)
        {
            app.UseHealthChecks((PathString) "/status/readiness", new HealthCheckOptions()
            {
                AllowCachingResponses = false,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
                ResultStatusCodes = {
                    [HealthStatus.Healthy] = 200,
                    [HealthStatus.Degraded] = 200,
                    [HealthStatus.Unhealthy] = 503
                }
            });
            
            app.UseHealthChecks((PathString) "/status/liveness", new HealthCheckOptions()
            {
                AllowCachingResponses = false,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
                Predicate = (Func<HealthCheckRegistration, bool>) (r => r.Name == "self"),
                ResultStatusCodes = {
                    [HealthStatus.Healthy] = 200,
                    [HealthStatus.Degraded] = 200,
                    [HealthStatus.Unhealthy] = 503
                }
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecksUI(opt =>
                {
                    opt.UIPath = "/status/health-ui";
                    opt.UseRelativeApiPath = true;
                });
            });
        }
    }
}