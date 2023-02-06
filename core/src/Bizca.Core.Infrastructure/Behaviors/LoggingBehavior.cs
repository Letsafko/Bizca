namespace Bizca.Core.Infrastructure.Behaviors
{
    using Extension;
    using Logging;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using Serilog.Context;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
        where TRequest : class, IBaseRequest, IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, 
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            IDictionary<string, string> properties = request.Crumble();
            using (LogContext.Push(new AggregateLogEnricher(properties)))
            {
                _logger.LogDebug("handling request {@Request}", request);

                TResponse response = await next();

                _logger.LogDebug("request {RequestName} handled", request.GetGenericTypeName());
                return response;
            }
        }
    }
}