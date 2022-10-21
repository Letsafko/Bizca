namespace Bizca.Core.Api.Modules.Filters;

using FluentValidation;
using Infrastructure.Extension;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

public class StartupConfigurationCheckFilter<T> : IStartupFilter where T : class, new()
{
    private readonly ILogger<StartupConfigurationCheckFilter<T>> _logger;
    private readonly IExceptionFormatter _exceptionFormatter;
    private readonly IValidator<T> _validator;
    private readonly IOptions<T> _options;

    public StartupConfigurationCheckFilter(IExceptionFormatter exceptionFormatter,
        IValidator<T> validator,
        IOptions<T> options,
        ILogger<StartupConfigurationCheckFilter<T>> logger)
    {
        _exceptionFormatter = exceptionFormatter;
        _validator = validator;
        _options = options;
        _logger = logger;
    }

    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
        _logger.LogDebug("Starting Validation of configuration for {ConfigurationName} ...",
            typeof(T).GetGenericTypeName());
        
        try
        {
            _validator.ValidateAndThrow(_options.Value);
            _logger.LogDebug("Validation of configuration for {ConfigurationName} ends successfully",
                typeof(T).GetGenericTypeName());
        
            return next;
        }
        catch (ValidationException e)
        {
            var errorMessage = _exceptionFormatter.Format(e.Errors);
            _logger.LogCritical("Validation of configuration for {ConfigurationName} failed with result: {Validation}", 
                errorMessage,
                typeof(T).GetGenericTypeName());
            
            throw;
        }
    }
}