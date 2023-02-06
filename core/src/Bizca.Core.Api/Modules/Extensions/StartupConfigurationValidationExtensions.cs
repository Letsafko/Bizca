namespace Bizca.Core.Api.Modules.Extensions
{
    using Filters;
    using Infrastructure.Database.Configuration;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class StartupConfigurationValidationExtensions
    {
        public static IServiceCollection AddStartupConfigurationCheck(this IServiceCollection services,
            IConfigurationSection configurationSection)
        {
            services.Configure<DatabaseConfiguration>(configurationSection);
            services.AddTransient<IStartupFilter, StartupConfigurationCheckFilter>();

            return services;
        }
    }
}