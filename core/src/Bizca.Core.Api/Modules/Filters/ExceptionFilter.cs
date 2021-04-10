namespace Bizca.Core.Api.Modules.Filters
{
    using Bizca.Core.Api.Modules.Extensions;
    using Bizca.Core.Domain.Exceptions;
    using FluentValidation;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Linq;

    /// <summary>
    ///     Exception Filter.
    /// </summary>
    public sealed class ExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// constructor <see cref="ExceptionFilter" />
        /// </summary>
        /// <param name="env"></param>
        /// <param name="logger"></param>
        public ExceptionFilter(IHostEnvironment env, ILogger<ExceptionFilter> logger)
        {
            this.logger = logger;
            this.env = env;
        }

        private readonly ILogger<ExceptionFilter> logger;
        private readonly IHostEnvironment env;

        /// <summary>
        ///     Add details when occurs an exception.
        /// </summary>
        public void OnException(ExceptionContext context)
        {
            if (context == null || context.Exception is null)
                return;

            context.ExceptionHandled = true;
            context.Result = GetModelStateResponse(env, context.Exception);
            logger.LogError(new EventId(context.Exception.HResult), context.Exception, context.Exception.Message);
        }

        #region private helpers

        private IActionResult GetModelStateResponse(IHostEnvironment environment, Exception exception)
        {
            string[] modelStateErrors;
            int statusCode = GetStatusCode(exception);
            if (!(exception is DomainException) && !(exception is ValidationException))
            {
                modelStateErrors = new string[] { "an error occured, contact your administrator." };
            }
            else if (exception is ValidationException validationException)
            {
                modelStateErrors = validationException.Errors
                    .ToLookup(x => x.PropertyName)
                    .ToDictionary(x => x.Key, y => y.Select(z => z.ErrorMessage).ToArray())
                    .SelectMany(x => x.Value)
                    .ToArray();
            }
            else
            {
                var domainException = exception as DomainException;
                modelStateErrors = domainException.Errors
                    .ToLookup(x => x.PropertyName)
                    .ToDictionary(x => x.Key, y => y.Select(z => z.ErrorMessage).ToArray())
                    .SelectMany(x => x.Value)
                    .ToArray();
            }

            var modelState = new ModelStateResponse(statusCode,
                modelStateErrors,
                !environment.IsDevEnvironment() ? default : exception);
            return new ObjectResult(modelState) { StatusCode = modelState.Status };
        }
        private bool IsAssignableFrom<T>(object obj) where T : class
        {
            return !(obj is null) && typeof(T).IsAssignableFrom(obj.GetType());
        }
        private int GetStatusCode(object obj)
        {
            return IsAssignableFrom<DomainException>(obj) || IsAssignableFrom<ValidationException>(obj)
                ? IsAssignableFrom<NotFoundDomainException>(obj) ? StatusCodes.Status404NotFound : StatusCodes.Status400BadRequest
                : StatusCodes.Status500InternalServerError;
        }

        #endregion
    }
}