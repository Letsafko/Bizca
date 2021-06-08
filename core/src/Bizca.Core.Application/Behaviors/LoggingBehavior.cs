namespace Bizca.Core.Application.Behaviors
{
    using Bizca.Core.Application.Enrichers;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using Serilog.Context;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : class
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            IDictionary<string, string> dicoProperties = ReflectionHelpers.Populate(request);
            using (LogContext.Push(new AggregateLogEnricher(dicoProperties)))
            {
                _logger.LogDebug("handling request {@request}.", request);

                TResponse response = await next();

                _logger.LogDebug("request {@requestType} handled.", request.GetType().Name);
                return response;
            }
        }
    }
}
