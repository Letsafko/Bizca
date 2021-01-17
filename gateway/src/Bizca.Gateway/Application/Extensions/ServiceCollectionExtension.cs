namespace Bizca.Gateway.Application.Extensions
{
    using Bizca.Core.Api.Modules.Extensions;
    using Bizca.Core.Security.Antelop.Extensions;
    using Bizca.Gateway.Application.Configuration;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Ocelot.DependencyInjection;
    using Ocelot.Provider.Consul;
    using Ocelot.Provider.Polly;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Extension class to configure services that needs to be added to the container
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtension
    {
        internal static IServiceCollection ConfigureServiceCollection(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddFoundationFeatures(configuration)
                .ConfigureOcelot(configuration)
                //.AddHealthChecks(configuration)
                .AddAntelop(configuration["Antelop:Certificate"]);
        }

        private static IServiceCollection AddFoundationFeatures(this IServiceCollection services, IConfiguration configuration)
        {
            Core.Api.Modules.Configuration.FeaturesConfigurationModel features = configuration.GetFeaturesConfiguration();

            if (features.ApplicationInsights)
            {
                services.AddApplicationInsights(configuration.GetApplicationInsightsConfiguration());
            }

            if (features.Sts)
            {
                //services.AddSts(configuration.GetStsConfiguration());
            }

            return services;
        }

        private static IServiceCollection ConfigureOcelot(this IServiceCollection services, IConfiguration configuration)
        {
            IOcelotBuilder ocelotBuilder = services
                .AddOcelot()
                .AddPolly();

            services.AddSwaggerForOcelot(configuration);

            ApiFeaturesConfigurationModel features = configuration.GetConfiguration<ApiFeaturesConfigurationModel>();
            if (features.Consul)
            {
                ocelotBuilder.AddConsul();
            }

            if (features.Caching)
            {
                services.AddCacheManagerConfiguration(configuration);
            }

            return services;
        }
    }
}