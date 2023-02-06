namespace Bizca.Core.Api.Modules.Filters
{
    using FluentValidation;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using System;
    using Infrastructure.Database.Configuration;

    public class StartupConfigurationCheckFilter : IStartupFilter
    {
        private readonly IExceptionFormatter _exceptionFormatter;
        private readonly ILogger<StartupConfigurationCheckFilter> _logger;
        private readonly IServiceProvider _serviceProvider;

        public StartupConfigurationCheckFilter(ILogger<StartupConfigurationCheckFilter> logger,
            IExceptionFormatter exceptionFormatter,
            IServiceProvider serviceProvider)
        {
            _exceptionFormatter = exceptionFormatter;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            const string typeName = nameof(DatabaseConfiguration);
            _logger.LogDebug("Starting Validation of configuration for {ConfigurationName} ...",
                typeName);

            try
            {
                var configuration = _serviceProvider.GetRequiredService<IOptions<DatabaseConfiguration>>().Value;
                var validator = _serviceProvider.GetRequiredService<IValidator<DatabaseConfiguration>>();

                validator.ValidateAndThrow(configuration);

                _logger.LogDebug("Validation of configuration for {ConfigurationName} ends successfully",
                    nameof(DatabaseConfiguration));

                return next;
            }
            catch (ValidationException e)
            {
                _logger.LogCritical("Validation of configuration for {ConfigurationName} failed with result: {Validation}",
                    _exceptionFormatter.Format(e.Errors),
                    typeName);

                throw;
            }
        }
    }
}