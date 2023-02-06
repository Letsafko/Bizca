namespace Bizca.Core.Infrastructure.Logging
{
    using Serilog.Core;
    using Serilog.Events;
    using System;
    using System.Collections.Generic;

    public sealed class AggregateLogEnricher : ILogEventEnricher
    {
        private readonly IDictionary<string, string> _properties;

        public AggregateLogEnricher(IDictionary<string, string> context)
        {
            _properties = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            foreach (KeyValuePair<string, string> property in _properties)
            {
                var logEventProperty = propertyFactory
                    .CreateProperty(property.Key, property.Value?.Replace("\"", ""));

                logEvent.AddPropertyIfAbsent(logEventProperty);
            }
        }
    }
}