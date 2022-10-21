namespace Bizca.Core.Api.Modules.Telemetry
{
    using Microsoft.ApplicationInsights;
    using System;
    using System.Collections.Generic;

    public sealed class ApplicationInsightsTelemetryService : ITelemetryService
    {
        private readonly TelemetryClient _telemetryClient;
        private bool _disposed;

        public ApplicationInsightsTelemetryService(TelemetryClient telemetryClient)
        {
            _telemetryClient = telemetryClient;
        }

        ~ApplicationInsightsTelemetryService() => Dispose(false);
        
        public void TrackEvent(string eventName, 
            IDictionary<string, string> properties = null,
            IDictionary<string, double> metrics = null)
        {
            _telemetryClient.TrackEvent(eventName, properties, metrics);
        }

        public void TrackMetric(string name, 
            double value, 
            IDictionary<string, string> properties = null)
        {
            _telemetryClient.TrackMetric(name, value, properties);
        }

        public void TrackTrace(string message)
        {
            _telemetryClient.TrackTrace(message);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        private void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
                _telemetryClient.Flush();

            _disposed = true;
        }
    }
}