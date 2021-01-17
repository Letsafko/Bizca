namespace Bizca.Core.Application.Behaviors
{
    using FluentValidation;
    using FluentValidation.Results;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IValidatorFactory validatorFactory;
        private readonly ILogger<ValidationBehavior<TRequest, TResponse>> logger;

        public ValidationBehavior(IValidatorFactory validatorFactory, ILogger<ValidationBehavior<TRequest, TResponse>> logger)
        {
            this.logger = logger;
            this.validatorFactory = validatorFactory;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            string typeName = request.GetType().Name;
            logger.LogDebug($"validating {typeName}");

            IValidator validator = validatorFactory.GetValidator(typeof(TRequest));
            ValidationResult result = validator.Validate(new ValidationContext<TRequest>(request));
            if (result?.IsValid == false)
            {
                IList<ValidationFailure> failures = result.Errors;
                logger.LogError($"validation errors - {typeName} - request: {@request} - errors: {@failures}");
                throw new ValidationException(failures);
            }

            logger.LogDebug($"{typeName} validated");
            return await next().ConfigureAwait(false);
        }
    }
}