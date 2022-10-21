namespace Bizca.Core.Api.Modules.Telemetry
{
    using System;
    using System.Collections.Generic;

    public interface ITelemetryService : IDisposable
    {
        /// <summary>
        ///     Send a telemetry event for display in Diagnostic Search and in the Analytics Portal.
        /// </summary>
        /// <remarks>
        ///     <a href="https://go.microsoft.com/fwlink/?linkid=525722#trackevent">Learn more</a>
        /// </remarks>
        /// <param name="eventName">A name for the event.</param>
        /// <param name="properties">Named string values you can use to search and classify events.</param>
        /// <param name="metrics">Measurements associated with this event.</param>
        void TrackEvent(string eventName, 
            IDictionary<string, string> properties = null,
            IDictionary<string, double> metrics = null);

        /// <summary>
        ///     This method is not the preferred method for sending metrics.
        ///     Metrics should always be pre-aggregated across a time period before being sent.<br />
        /// </summary>
        /// <param name="name">Metric name.</param>
        /// <param name="value">Metric value.</param>
        /// <param name="properties">Named string values you can use to classify and filter metrics.</param>
        void TrackMetric(string name, 
            double value, 
            IDictionary<string, string> properties = null);

        /// <summary>
        ///     Send a trace message for display in Diagnostic Search.
        /// </summary>
        /// <remarks>
        ///     <a href="https://go.microsoft.com/fwlink/?linkid=525722#tracktrace">Learn more</a>
        /// </remarks>
        /// <param name="message">Message to display.</param>
        void TrackTrace(string message);
    }
}