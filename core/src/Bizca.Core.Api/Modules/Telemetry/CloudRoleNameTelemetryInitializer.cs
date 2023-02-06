namespace Bizca.Core.Api.Modules.Telemetry
{
    using Microsoft.ApplicationInsights.Channel;
    using Microsoft.ApplicationInsights.Extensibility;
    using Microsoft.Extensions.Configuration;
    using System;

    public class CloudRoleNameTelemetryInitializer : ITelemetryInitializer
    {
        private const string ApplicationNameScheme = "Api:ApplicationInsights:ApplicationName";
        private readonly string _cloudRoleName;

        public CloudRoleNameTelemetryInitializer(IConfiguration configuration)
        {
            _cloudRoleName = configuration[ApplicationNameScheme];
        }

        public void Initialize(ITelemetry telemetry)
        {
            if (string.IsNullOrWhiteSpace(_cloudRoleName))
                throw new InvalidOperationException("missing application name configuration.");

            telemetry.Context.Cloud.RoleName = _cloudRoleName;
        }
    }
}