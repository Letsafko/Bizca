namespace Bizca.Core.Api.Modules.Filters
{
    using Domain;
    using Domain.Exceptions;
    using Extensions;
    using FluentValidation;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using System;

    public sealed class ExceptionFilter : IExceptionFilter
    {
        private readonly IExceptionFormatter _exceptionFormatter;
        private readonly ILogger<ExceptionFilter> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionFilter(IHostEnvironment env, 
            ILogger<ExceptionFilter> logger, 
            IExceptionFormatter exceptionFormatter)
        {
            _exceptionFormatter = exceptionFormatter;
            _logger = logger;
            _env = env;
        }

        public void OnException(ExceptionContext context)
        {
            if (context?.Exception is null)
                return;

            context.ExceptionHandled = true;
            context.Result = GetModelStateResponse(_env, context.Exception);
            
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

            errorMessage = !environment.IsDevEnvironment()
                ? errorMessage
                : JsonConvert.SerializeObject(exception);
            
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