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
    using Newtonsoft.Json;
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
            string modelStateError;
            int statusCode = GetStatusCode(exception);
            if (!(exception is DomainException) && !(exception is ResourceNotFoundException) && !(exception is ValidationException))
            {
                modelStateError = "an error occured, contact your administrator.";
            }
            else if (exception is ValidationException validationException)
            {
                modelStateError = validationException.Errors.FirstOrDefault()?.ErrorMessage;
            }
            else if (exception is DomainException domainException)
            {
                modelStateError = domainException.Errors.FirstOrDefault()?.ErrorMessage;
            }
            else
            {
                modelStateError = (exception as ResourceNotFoundException).Errors.FirstOrDefault()?.ErrorMessage;
            }

            var error = !environment.IsDevEnvironment() ? modelStateError : JsonConvert.SerializeObject(exception);
            var modelState = new ModelStateResponse(statusCode, error);
            return new ObjectResult(modelState)
            {
                StatusCode = modelState.ErrorCode
            };
        }
        private bool IsAssignableFrom<T>(object obj) where T : class
        {
            return !(obj is null) && typeof(T).IsAssignableFrom(obj.GetType());
        }
        private int GetStatusCode(object obj)
        {
            return IsAssignableFrom<DomainException>(obj) || IsAssignableFrom<ValidationException>(obj)
                ? StatusCodes.Status400BadRequest
                : (IsAssignableFrom<ResourceNotFoundException>(obj) ? StatusCodes.Status404NotFound : StatusCodes.Status500InternalServerError);
        }

        #endregion
    }
}