namespace Bizca.Core.Api.Modules.Telemetry
{
    using Microsoft.ApplicationInsights;
    using System;
    using System.Collections.Generic;

    public class ApplicationInsightsTelemetryService : ITelemetryService
    {
        private readonly TelemetryClient telemetryClient;
        private bool disposed;
        public ApplicationInsightsTelemetryService(TelemetryClient telemetryClient)
        {
            this.telemetryClient = telemetryClient ?? throw new ArgumentNullException(nameof(telemetryClient));
        }

        ~ApplicationInsightsTelemetryService() => Dispose(false);

        public void TrackEvent(string eventName, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
        {
            telemetryClient.TrackEvent(eventName, properties, metrics);
        }

        public void TrackMetric(string name, double value, IDictionary<string, string> properties = null)
        {
            telemetryClient.TrackMetric(name, value, properties);
        }

        public void TrackTrace(string message)
        {
            telemetryClient.TrackTrace(message);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
                telemetryClient.Flush();

            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}