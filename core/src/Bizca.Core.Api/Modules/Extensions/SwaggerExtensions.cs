namespace Bizca.Core.Api.Modules.Extensions
{
    using Configuration;
    using Filters;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;

    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services,
            SwaggerConfigurationModel swaggerConfiguration, 
            Action<SwaggerGenOptions> specificSetupAction = null)
        {
            if (swaggerConfiguration.Versions?.Any() != true)
                throw new InvalidOperationException($"missing {nameof(swaggerConfiguration.Versions)} configuration");

            services.AddSwaggerGen(x =>
            {
                foreach (VersionConfigurationModel current in swaggerConfiguration.Versions)
                    x.SwaggerDoc($"v{current.Version}",
                        new OpenApiInfo
                        {
                            Version = $"v{current.Version}",
                            Title = current.Title,
                            Description = current.Description,
                            Contact = new OpenApiContact { Email = current.Email }
                        });

                x.OrderActionsBy(criteria => criteria.RelativePath!.Length.ToString());
                x.AddSwaggerStsSecurity(swaggerConfiguration.StsSecurity);
                x.AddSwaggerSecurity(swaggerConfiguration.Security);

                x.DocumentFilter<ReplaceVersionWithExactValueInPathFilter>();
                x.OperationFilter<SwaggerExcludePropertyFilter>();
                x.OperationFilter<RemoveVersionFromParameter>();

                x.DocInclusionPredicate((version, apiDescriptor) =>
                {
                    if (!apiDescriptor.TryGetMethodInfo(out MethodInfo methodInfo))
                        return false;

                    if (!Regex.IsMatch(apiDescriptor.RelativePath!, "v{version}") &&
                        !Regex.IsMatch(apiDescriptor.RelativePath, @"v(\d+\.)?(\d+\.)?(\*|\d+)"))
                        return false;

                    IEnumerable<ApiVersion> versions = methodInfo
                        .DeclaringType!
                        .GetCustomAttributes(true)
                        .OfType<ApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions)
                        .ToArray();
                    
                    ApiVersion[] maps = methodInfo
                        .GetCustomAttributes()
                        .OfType<MapToApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions)
                        .ToArray();
                    
                    return versions.Any(apiVersion => $"v{apiVersion}" == version) &&
                           (maps.Length == 0 || maps.Any(v => $"v{v}" == version));
                });

                var xmlDocumentations 
                    = swaggerConfiguration.XmlDocumentations 
                      ?? new[] { $"{Assembly.GetEntryAssembly()?.GetName().Name}.xml" };
                
                foreach (string documentationPath in xmlDocumentations)
                {
                    string xmlPath = Path.Combine(AppContext.BaseDirectory, documentationPath);
                    x.IncludeXmlComments(xmlPath);
                }

                x.TagActionsBy(api =>
                {
                    return !string.IsNullOrWhiteSpace(api.GroupName)
                        ? new[] { api.GroupName }
                        : api.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor
                            ? new[] { controllerActionDescriptor.ControllerName }
                            : throw new InvalidOperationException("Unable to determine tag for endpoint.");
                });

                specificSetupAction?.Invoke(x);
            });

            return services;
        }
    }
}