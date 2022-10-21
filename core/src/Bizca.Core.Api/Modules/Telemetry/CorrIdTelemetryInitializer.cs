namespace Bizca.Core.Api.Modules.Telemetry
{
    using Microsoft.ApplicationInsights.Channel;
    using Microsoft.ApplicationInsights.DataContracts;
    using Microsoft.ApplicationInsights.Extensibility;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Primitives;
    using System;
    using System.Collections.Generic;

    public class CorrIdTelemetryInitializer : ITelemetryInitializer
    {
        private const string CorrIdKey = "CorrId";
        private readonly IHttpContextAccessor _contextAccessor;

        public CorrIdTelemetryInitializer(IHttpContextAccessor contextAccessor)
        {
            this._contextAccessor = contextAccessor;
        }

        public void Initialize(ITelemetry telemetry)
        {
            HttpContext context = _contextAccessor?.HttpContext;
            if (context == null || telemetry is not ISupportProperties supportProperties)
                return;

            StringValues headers = context.Request.Headers[CorrIdKey];
            if (headers.Count <= 0) 
                return;
            
            IDictionary<string, string> properties = supportProperties.Properties;
            if (!properties.ContainsKey(CorrIdKey))
                properties.Add(CorrIdKey,
                    string.Join(Environment.NewLine, headers));
        }
    }
}