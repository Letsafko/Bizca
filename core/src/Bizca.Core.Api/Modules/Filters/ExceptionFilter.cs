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
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    ///     Exception Filter.
    /// </summary>
    public sealed class ExceptionFilter : IExceptionFilter
    {
        private readonly IHostEnvironment env;
        private readonly ILogger<ExceptionFilter> logger;

        /// <summary>
        /// constructor <see cref="ExceptionFilter" />
        /// </summary>
        /// <param name="env"></param>
        /// <param name="logger"></param>
        public ExceptionFilter(IHostEnvironment env, ILogger<ExceptionFilter> logger)
        {
            this.env = env;
            this.logger = logger;
        }

        /// <summary>
        ///     Add details when occurs an exception.
        /// </summary>
        public void OnException(ExceptionContext context)
        {
            BuildExceptionContext(context);
            logger.LogError(new EventId(context.Exception.HResult), context.Exception, context.Exception.Message);
        }

        private void BuildExceptionContext(ExceptionContext context)
        {
            if (context == null)
                return;

            context.ExceptionHandled = true;
            ModelStateResponse modelState;
            switch (context.Exception)
            {
                case DomainException _:
                case ValidationException _:
                    modelState = GetErros(context.Exception);
                    context.Result = new BadRequestObjectResult(modelState) { StatusCode = StatusCodes.Status400BadRequest };
                    break;

                default:
                    const string errorMessage = "an error occured, contact your administrator.";
                    modelState = new ModelStateResponse(StatusCodes.Status500InternalServerError,
                        new string[] { errorMessage },
                        !env.IsDevEnvironment() ? default : context.Exception);

                    context.Result = new ObjectResult(modelState) { StatusCode = StatusCodes.Status500InternalServerError };
                    break;
            }
        }

        private ModelStateResponse GetErros(Exception exception)
        {
            Dictionary<string, string[]> modelState = null;
            if (exception is DomainException domainException)
            {
                modelState = domainException.Errors
                    .ToLookup(x => x.PropertyName)
                    .ToDictionary(x => x.Key, y => y.Select(z => z.ErrorMessage).ToArray());
            }
            else if (exception is ValidationException validationException)
            {
                modelState = validationException.Errors
                    .ToLookup(x => x.PropertyName)
                    .ToDictionary(x => x.Key, y => y.Select(z => z.ErrorMessage).ToArray());
            }

            return new ModelStateResponse(StatusCodes.Status400BadRequest,
                         modelState.SelectMany(x => x.Value),
                         !env.IsDevEnvironment() ? default : exception);
        }
    }
}
