namespace Bizca.Core.Infrastructure.Behaviors
{
    using Extension;
    using FluentValidation;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;
        private readonly IServiceProvider _serviceProvider;

        public ValidationBehavior(IServiceProvider serviceProvider,
            ILogger<ValidationBehavior<TRequest, TResponse>> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            string requestName = request.GetGenericTypeName();
            var validator = _serviceProvider.GetService<IValidator<TRequest>>();

            if (validator is null)
            {
                _logger.LogDebug("Request {RequestName} will not be validated since no validator was found",
                    requestName);
            }
            else
            {
                _logger.LogDebug("Validating request {RequestName}:{@Request}", requestName, request);

                try
                {
                    await validator.ValidateAndThrowAsync(request, cancellationToken);
                }
                catch (ValidationException exception)
                {
                    _logger.LogWarning(exception, "Validation failed for request {RequestName}", requestName);
                    throw;
                }

                _logger.LogDebug("Validation of request {RequestName} ends successfully", requestName);
            }

            return await next();
        }
    }
}