namespace Bizca.Core.Api.Modules.Extensions
{
    using Bizca.Core.Api.Modules.Configuration;
    using Bizca.Core.Api.Modules.Conventions;
    using Bizca.Core.Api.Modules.Filters;
    using Bizca.Core.Api.Modules.Presentation.HttpStrategies;
    using Bizca.Core.Api.Modules.Telemetry;
    using Bizca.Core.Domain.Cache;
    using Bizca.Core.Domain.Services;
    using Bizca.Core.Infrastructure.Cache;
    using IdentityServer4.AccessTokenValidation;
    using Microsoft.ApplicationInsights.Extensibility;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc.Versioning;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;

    public static class ServiceCollectionExtensions
    {
        internal static IServiceCollection ConfigureServiceCollection(this IServiceCollection services, IConfiguration configuration)
        {
            FeaturesConfigurationModel features = configuration.GetFeaturesConfiguration();
            if (features.Logging)
            {
                services.AddLogging();
            }

            if (features.ApplicationInsights)
            {
                services.AddApplicationInsights(configuration.GetApplicationInsightsConfiguration());
            }

            if (features.Cors)
            {
                services.AddCors(configuration.GetCorsConfiguration());
            }

            if (features.Sts)
            {
                services.AddSts(configuration.GetStsConfiguration());
            }

            if (features.Versioning)
            {
                services.AddVersioning(configuration.GetVersioningConfiguration());
            }

            if (features.Swagger)
            {
                services.AddSwagger(configuration.GetSwaggerConfiguration(), opt => opt.DocumentFilter<MarkdownFileResolverFilter>());
            }

            return
                services
                    .AddCache()
                    .AddServices()
                    .AddHttpStrategies();
        }

        #region private helpers

        private static IServiceCollection AddSwagger(this IServiceCollection services, SwaggerConfigurationModel swaggerConfiguration, Action<SwaggerGenOptions> specificSetupAction)
        {
            if (swaggerConfiguration.Versions?.Any() != true)
            {
                throw new MissingConfigurationException(nameof(swaggerConfiguration.Versions));
            }

            services.AddSwaggerGen(x =>
            {
                foreach (VersionConfigurationModel current in swaggerConfiguration.Versions)
                {
                    x.SwaggerDoc($"v{current.Version}", new OpenApiInfo
                    {
                        Version = $"v{current.Version}",
                        Title = current.Title,
                        Description = current.Description,
                        Contact = new OpenApiContact
                        {
                            Email = current.Email
                        }
                    });
                }

                x.OrderActionsBy(x => x.RelativePath.Length.ToString());
                x.AddSwaggerStsSecurity(swaggerConfiguration.StsSecurity);
                x.AddSwaggerSecurity(swaggerConfiguration.Security);

                x.DocumentFilter<ReplaceVersionWithExactValueInPath>();
                x.OperationFilter<SwaggerExcludePropertyFilter>();
                x.OperationFilter<RemoveVersionFromParameter>();

                x.DocInclusionPredicate((version, apiDescriptor) =>
                {
                    if (!apiDescriptor.TryGetMethodInfo(out MethodInfo mi))
                        return false;

                    if (!Regex.IsMatch(apiDescriptor.RelativePath, "v{version}") && !Regex.IsMatch(apiDescriptor.RelativePath, @"v(\d+\.)?(\d+\.)?(\*|\d+)"))
                        return false;

                    IEnumerable<ApiVersion> versions = mi.DeclaringType.GetCustomAttributes(true).OfType<ApiVersionAttribute>().SelectMany(attr => attr.Versions);
                    ApiVersion[] maps = mi.GetCustomAttributes().OfType<MapToApiVersionAttribute>().SelectMany(attr => attr.Versions).ToArray();
                    return versions.Any(v => $"v{v}" == version) && (maps.Length == 0 || maps.Any(v => $"v{v}" == version));
                });

                foreach (string documentationPath in swaggerConfiguration.XmlDocumentations ?? new[] { $"{Assembly.GetEntryAssembly().GetName().Name}.xml" })
                {
                    string xmlPath = Path.Combine(AppContext.BaseDirectory, documentationPath);
                    x.IncludeXmlComments(xmlPath);
                }

                x.TagActionsBy(api =>
                {
                    return !string.IsNullOrWhiteSpace(api.GroupName)
                        ? (new[] { api.GroupName })
                        : api.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor
                        ? (new[] { controllerActionDescriptor.ControllerName })
                        : throw new InvalidOperationException("Unable to determine tag for endpoint.");
                });

                specificSetupAction?.Invoke(x);
            });

            return services;
        }
        private static IServiceCollection AddApplicationInsights(this IServiceCollection services, ApplicationInsightsConfigurationModel applicationInsightsConfiguration)
        {
            services.AddSingleton<ITelemetryInitializer, CloudRoleNameTelemetryInitializer>();
            services.AddSingleton<ITelemetryInitializer, CorrIdTelemetryInitializer>();

            applicationInsightsConfiguration.EnableActiveTelemetryConfigurationSetup = true;
            applicationInsightsConfiguration.EnableAdaptiveSampling = false;
            return services.AddApplicationInsightsTelemetry(applicationInsightsConfiguration)
                           .AddSingleton<ITelemetryService, ApplicationInsightsTelemetryService>();
        }
        private static IServiceCollection AddVersioning(this IServiceCollection services, VersioningConfigurationModel versioningConfiguration)
        {
            if (string.IsNullOrWhiteSpace(versioningConfiguration.RouteConstraintName))
            {
                throw new MissingConfigurationException(nameof(versioningConfiguration.RouteConstraintName));
            }

            void mvcOptions(MvcOptions x)
            {
                x.Conventions.Add(new DefaultApiVersionConvention(versioningConfiguration.RouteConstraintName));
            }

            services.Configure((Action<MvcOptions>)mvcOptions);
            if (string.IsNullOrWhiteSpace(versioningConfiguration.Default))
            {
                throw new MissingConfigurationException(nameof(versioningConfiguration.Default));
            }

            services.AddApiVersioning(opts =>
            {
                opts.DefaultApiVersion = ApiVersion.Parse(versioningConfiguration.Default);
                opts.AssumeDefaultVersionWhenUnspecified = true;
                opts.ApiVersionReader = new UrlSegmentApiVersionReader();
                opts.RouteConstraintName = versioningConfiguration.RouteConstraintName;
            });
            return services;
        }
        private static IServiceCollection AddSwagger(this IServiceCollection services, SwaggerConfigurationModel swaggerConfiguration)
        {
            return services.AddSwagger(swaggerConfiguration, null);
        }
        private static IServiceCollection AddCors(this IServiceCollection services, CorsConfigurationModel corsConfiguration)
        {
            return services.AddCors(options =>
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
        private static IServiceCollection AddSts(this IServiceCollection services, StsConfiguration stsConfig)
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
            return services;
        }
        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            return
                services
                    .AddSingleton<IDateService, DateService>();
        }
        private static IServiceCollection AddCache(this IServiceCollection services)
        {
            return
                services
                    .AddMemoryCache()
                    .AddSingleton<ICacheProvider, MemoryCacheProvider>();
        }
        private static IServiceCollection AddHttpStrategies(this IServiceCollection services)
        {
            return
                services
                    .AddSingleton<IHttpStrategy, InternalServerErrorStrategy>()
                    .AddSingleton<IHttpStrategy, BadRequestStrategy>()
                    .AddSingleton<IHttpStrategy, NotFoundStrategy>()
                    .AddSingleton<IHttpStrategyFactory, HttpStrategyFactory>();
        }

        #endregion
    }
}