namespace Bizca.Core.Api.Modules.Filters
{
    using Domain;
    using Domain.Exceptions;
    using Domain.Rules.Exception;
    using FluentValidation;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using System;

    public sealed class ExceptionFilter : IExceptionFilter
    {
        private readonly IExceptionFormatter _exceptionFormatter;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(IHostEnvironment hostEnvironment, 
            ILogger<ExceptionFilter> logger, 
            IExceptionFormatter exceptionFormatter)
        {
            _exceptionFormatter = exceptionFormatter;
            _hostEnvironment = hostEnvironment;
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (context?.Exception is null)
                return;

            context.ExceptionHandled = true;
            context.Result = GetModelStateResponse(_hostEnvironment, context.Exception);
            
            _logger.LogError(new EventId(context.Exception.HResult), 
                context.Exception, 
                "{Message}", 
                context.Exception.Message);
        }

        private IActionResult GetModelStateResponse(IHostEnvironment environment, Exception exception)
        {
            (string errorMessage, string errorCode) = exception switch
            {
                ValidationException validationException
                    => (_exceptionFormatter.Format(validationException.Errors), "invalid_input"),
                
                DomainException domainException => (_exceptionFormatter.Format(domainException.Message,
                    domainException.Errors), domainException.ErrorCode),
                
                ResourceNotFoundException resourceNotFoundException => (resourceNotFoundException.Message,
                    resourceNotFoundException.ErrorCode),
                
                _ => ("an error occured, contact your administrator", "internal_error")
            };

            var statusCode = GetStatusCode(exception);
            var modelState = new PublicResponse<object>(data: null,
                statusCode,
                message: errorMessage, 
                errorCode: errorCode);
            
            return new ObjectResult(modelState) { StatusCode = statusCode };
        }

        private static bool IsAssignableFrom<T>(object obj) where T : class
        {
            return obj is T;
        }

        private static int GetStatusCode(object obj)
        {
            return IsAssignableFrom<DomainException>(obj) || IsAssignableFrom<ValidationException>(obj)
                ? StatusCodes.Status400BadRequest
                : IsAssignableFrom<ResourceNotFoundException>(obj)
                    ? StatusCodes.Status404NotFound
                    : StatusCodes.Status500InternalServerError;
        }
    }
}