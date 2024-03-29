﻿namespace Bizca.Core.Api.Modules.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using Prometheus;

    /// <summary>
    ///     Http Metrics Extensions.
    /// </summary>
    public static class HttpMetricsExtensions
    {
        /// <summary>
        ///     Add Prometheus dependencies.
        /// </summary>
        public static IApplicationBuilder UseCustomHttpMetrics(this IApplicationBuilder appBuilder)
        {
            return appBuilder.UseMetricServer()
                .UseHttpMetrics(options =>
                {
                    options.RequestDuration.Enabled = false;
                    options.InProgress.Enabled = false;
                    options.RequestCount.Counter = Metrics.CreateCounter(
                    "http_requests_total",
                    "HTTP Requests Total",
                    new CounterConfiguration { LabelNames = new[] { "controller", "method", "code" } });
                });
        }
    }
}