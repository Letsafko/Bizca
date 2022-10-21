namespace Bizca.Core.Api.Modules.Extensions;

using Infrastructure.Extension;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class StartupConfigurationExtensions
{
    public static IServiceCollection ConfigureFluentOptions<TOptions>(
        this IServiceCollection services,
        IConfiguration config) where TOptions : class, new()
    {
        var section = typeof(TOptions).GetGenericTypeName();
        //return AddFluentOptions<TOptions>(services, config, section, configurationExpression);

        return services;
    }
}