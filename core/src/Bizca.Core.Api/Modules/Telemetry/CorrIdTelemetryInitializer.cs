namespace Bizca.Core.Api.Modules.Telemetry
{
    using Microsoft.ApplicationInsights.Channel;
    using Microsoft.ApplicationInsights.DataContracts;
    using Microsoft.ApplicationInsights.Extensibility;
    using Microsoft.AspNetCore.Http;
    using System;

    /// <summary>Get the CorrId in header to log the key in global property</summary>
    public class CorrIdTelemetryInitializer : ITelemetryInitializer
    {
        private const string CorrIdKey = "CorrId";
        private readonly IHttpContextAccessor contextAccessor;

        /// <summary>Initializes a new instance of the <see cref="CorrIdTelemetryInitializer" /> class.</summary>
        /// <param name="contextAccessor">The context accessor.</param>
        public CorrIdTelemetryInitializer(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }

        /// <summary>Initializes properties of the specified <see cref="T:Microsoft.ApplicationInsights.Channel.ITelemetry">ITelemetry</see> object.</summary>
        /// <param name="telemetry"></param>
        public void Initialize(ITelemetry telemetry)
        {
            HttpContext context = contextAccessor?.HttpContext;

            if (context == null
                || !(telemetry is ISupportProperties))
            {
                return;
            }

            Microsoft.Extensions.Primitives.StringValues headers = context.Request.Headers[CorrIdKey];
            if (headers.Count > 0)
            {
                System.Collections.Generic.IDictionary<string, string> properties = ((ISupportProperties)telemetry).Properties;

                if (!properties.ContainsKey(CorrIdKey))
                {
                    properties.Add(CorrIdKey,
                     string.Join(Environment.NewLine, headers));
                }
            }
        }
    }
}