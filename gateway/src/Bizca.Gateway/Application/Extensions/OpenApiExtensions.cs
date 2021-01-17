namespace Bizca.Gateway.Application.Extensions
{
    using Bizca.Core.Api.Modules.Extensions;
    using Microsoft.Extensions.Configuration;
    using Microsoft.OpenApi.Models;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    /// <summary>Open Api extension methods</summary>
    [ExcludeFromCodeCoverage]
    public static class OpenApiExtensions
    {
        /// <summary>Adds the security configuration.</summary>
        /// <param name="document">The document.</param>
        /// <param name="securityDefinitionName">Name of the security definition.</param>
        /// <param name="securitySchema">The security schema.</param>
        public static void AddSecurityConfiguration(this OpenApiDocument document, string securityDefinitionName, OpenApiSecurityScheme securitySchema)
        {
            document.Components.SecuritySchemes.Add(securityDefinitionName, securitySchema);

            document.SecurityRequirements.Add(new OpenApiSecurityRequirement()
                    {
                        {
                            new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference()
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = securityDefinitionName
                                    }
                                }, new List<string>()
                        }
                    });
        }

        /// <summary>Excludes the partner code.</summary>
        /// <param name="document">The document.</param>
        /// <returns></returns>
        public static OpenApiDocument ExcludePartnerCode(this OpenApiDocument document)
        {
            foreach (KeyValuePair<string, OpenApiPathItem> path in document.Paths)
            {
                if (path.Value == null)
                {
                    continue;
                }

                foreach (KeyValuePair<OperationType, OpenApiOperation> operation in path.Value.Operations)
                {
                    OpenApiParameter partnerCodeParameter =
                        operation.Value.Parameters.FirstOrDefault(p => p.Name.ToLower() == Constants.SWAGGER_PARTNER_CODE_PARAMETER);
                    if (partnerCodeParameter != null
                        && !path.Key.ToLower().Contains(Constants.SWAGGER_PARTNER_CODE_PARAMETER))
                    {
                        operation.Value.Parameters.Remove(partnerCodeParameter);
                    }
                }
            }

            return document;
        }

        /// <summary>Adds the open API security scheme.</summary>
        /// <param name="document">The document.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static OpenApiDocument AddOpenApiSecurityScheme(this OpenApiDocument document,
            IConfiguration configuration)
        {
            Core.Api.Modules.Configuration.SwaggerConfigurationModel swaggerConfiguration = configuration.GetSwaggerConfiguration();

            if (swaggerConfiguration?.Security != null)
            {
                foreach (Core.Api.Modules.Configuration.SwaggerSecurityDefinitionModel securityDefinition in swaggerConfiguration.Security)
                {
                    Enum.TryParse(securityDefinition.Type, true, out SecuritySchemeType type);
                    Enum.TryParse(securityDefinition.In, true, out ParameterLocation location);

                    var securitySchema = new OpenApiSecurityScheme
                    {
                        Name = securityDefinition.Name,
                        Description = securityDefinition.Description,
                        Type = type,
                        Scheme = securityDefinition.Scheme,
                        In = location
                    };

                    document.AddSecurityConfiguration(securityDefinition.DisplayName, securitySchema);
                }
            }

            return document;
        }

        /// <summary>Adds the open API o authentication flow.</summary>
        /// <param name="document">The document.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static OpenApiDocument AddOpenApiOAuthFlow(this OpenApiDocument document,
            IConfiguration configuration)
        {
            Core.Api.Modules.Configuration.SwaggerConfigurationModel swaggerConfiguration = configuration.GetSwaggerConfiguration();

            if (configuration.GetFeaturesConfiguration().Sts && swaggerConfiguration?.StsSecurity != null)
            {
                Core.Api.Modules.Configuration.StsConfiguration stsConfiguration = configuration.GetStsConfiguration();

                var securitySchema = new OpenApiSecurityScheme
                {
                    Name = "Sts authentication",
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        ClientCredentials = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri($"{stsConfiguration.Authority}/connect/token"),
                            Scopes = swaggerConfiguration?.StsSecurity.Scopes.ToDictionary(k => k, v => string.Empty)
                        }
                    }
                };

                document.AddSecurityConfiguration(securitySchema.Name, securitySchema);
            }

            return document;
        }
    }
}