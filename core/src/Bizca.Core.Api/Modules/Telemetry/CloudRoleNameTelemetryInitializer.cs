namespace Bizca.Core.Api.Modules.Telemetry
{
    using Microsoft.ApplicationInsights.Channel;
    using Microsoft.ApplicationInsights.Extensibility;
    using Microsoft.Extensions.Configuration;

    /// <summary>Set the role name with System name configuration</summary>
    public class CloudRoleNameTelemetryInitializer : ITelemetryInitializer
    {
        private readonly string cloudRoleName;

        /// <summary>Initializes a new instance of the <see cref="CloudRoleNameTelemetryInitializer" /> class.</summary>
        /// <param name="configuration">The configuration.</param>
        public CloudRoleNameTelemetryInitializer(IConfiguration configuration)
        {
            cloudRoleName = configuration[ApplicationNameScheme];
        }

        private const string ApplicationNameScheme = "Api:ApplicationInsights:ApplicationName";

        /// <summary>Initializes properties of the specified <see cref="T:Microsoft.ApplicationInsights.Channel.ITelemetry">ITelemetry</see> object.</summary>
        /// <param name="telemetry"></param>
        public void Initialize(ITelemetry telemetry)
        {
            if (string.IsNullOrWhiteSpace(cloudRoleName))
            {
                throw new MissingConfigurationException("missing application name configuration.");
            }

            telemetry.Context.Cloud.RoleName = cloudRoleName;
        }
    }
}