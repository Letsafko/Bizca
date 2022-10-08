namespace Bizca.Core.Application.Enrichers
{
    using Serilog.Core;
    using Serilog.Events;
    using System;
    using System.Collections.Generic;

    public sealed class AggregateLogEnricher : ILogEventEnricher
    {
        private readonly IDictionary<string, string> context;

        /// <summary>
        ///     enrich each log with properties of context.
        /// </summary>
        /// <param name="context">each element of context represent a log enricher.</param>
        public AggregateLogEnricher(IDictionary<string, string> context)
        {
            if (context is null) throw new ArgumentNullException(nameof(context));
            this.context = context;
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            foreach (KeyValuePair<string, string> kv in context)
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(kv.Key, kv.Value?.Replace("\"", "")));
        }
    }
}