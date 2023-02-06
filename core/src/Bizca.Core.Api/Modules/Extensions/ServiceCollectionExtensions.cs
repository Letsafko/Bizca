namespace Bizca.Core.Api.Modules.Extensions
{
    using Configuration;
    using Filters;
    using IdentityServer4.AccessTokenValidation;
    using Infrastructure.Cache;
    using Microsoft.ApplicationInsights.AspNetCore.Extensions;
    using Microsoft.ApplicationInsights.Extensibility;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Presentation.HttpStrategies;
    using Telemetry;

    public static class ServiceCollectionExtensions
    {
        internal static IServiceCollection ConfigureServiceCollection(this IServiceCollection services,
            IConfiguration configuration)
        {
            FeaturesConfigurationModel features = configuration.GetFeaturesConfiguration();
            if (features.Logging) 
                services.AddLogging();

            if (features.ApplicationInsights)
                services.AddApplicationInsights(configuration.GetApplicationInsightsConfiguration());

            if (features.Cors) services.AddCors(configuration.GetCorsConfiguration());

            if (features.Sts) services.AddSts(configuration.GetStsConfiguration());

            if (features.Versioning) services.AddVersioning(configuration.GetVersioningConfiguration());

            if (features.Swagger)
                services.AddSwagger(configuration.GetSwaggerConfiguration(),
                    opt => opt.DocumentFilter<MarkdownFileResolverFilter>());

            return
                services
                    .AddCache()
                    .AddServices()
                    .AddHttpStrategies();
        }

        private static void AddApplicationInsights(this IServiceCollection services,
            ApplicationInsightsServiceOptions applicationInsightsConfiguration)
        {
            services.AddSingleton<ITelemetryInitializer, CloudRoleNameTelemetryInitializer>();
            services.AddSingleton<ITelemetryInitializer, CorrIdTelemetryInitializer>();

            applicationInsightsConfiguration.EnableActiveTelemetryConfigurationSetup = true;
            applicationInsightsConfiguration.EnableAdaptiveSampling = false;
        
            services
                .AddApplicationInsightsTelemetry(applicationInsightsConfiguration)
                .AddSingleton<ITelemetryService, ApplicationInsightsTelemetryService>();
        }

        private static void AddCors(this IServiceCollection services,
            CorsConfigurationModel corsConfiguration)
        {
            services
                .AddCors(options =>
                {
                    options.AddPolicy(nameof(corsConfiguration.DefaultApiPolicy), builder =>
                    {
                        builder
                            .WithOrigins(corsConfiguration.DefaultApiPolicy.Origins)
                            .WithMethods(corsConfiguration.DefaultApiPolicy.Methods)
                            .WithHeaders(corsConfiguration.DefaultApiPolicy.Headers);
                    });
                });
        }

        private static void AddSts(this IServiceCollection services, StsConfiguration stsConfig)
        {
            services
                .AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(stsConfig.Provider, options =>
                {
                    options.Authority = stsConfig.Authority;
                    options.ApiName = stsConfig.ApiName;
                    options.ApiSecret = stsConfig.ApiSecret;
                    options.EnableCaching = stsConfig.EnableCaching;
                    options.CacheDuration = stsConfig.CacheDuration;
                });
        }

        private static IServiceCollection AddHttpStrategies(this IServiceCollection services)
        {
            return
                services
                    .AddSingleton<IHttpStrategy, InternalServerErrorStrategy>()
                    .AddSingleton<IHttpStrategyFactory, HttpStrategyFactory>()
                    .AddSingleton<IHttpStrategy, BadRequestStrategy>()
                    .AddSingleton<IHttpStrategy, NotFoundStrategy>();
        }
        
        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            return
                services
                    .AddSingleton<ISystemClock, SystemClock>()
                    .AddSingleton<IExceptionFormatter, JsonSerializerExceptionFormatter>();
        }

        private static IServiceCollection AddCache(this IServiceCollection services)
        {
            return
                services
                    .AddMemoryCache()
                    .AddSingleton<ICacheProvider, MemoryCacheProvider>();
        }
    }
}