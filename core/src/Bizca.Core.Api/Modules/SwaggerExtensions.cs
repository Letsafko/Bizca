namespace Bizca.Core.Api.Modules
{
    using Bizca.Core.Api.Modules.Configuration;
    using Bizca.Core.Api.Modules.Filters;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.Filters;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class SwaggerExtensions
    {
        internal static void AddSwaggerSecurity(this SwaggerGenOptions opts, IEnumerable<SwaggerSecurityDefinitionModel> security)
        {
            if (security is null)
                return;

            SwaggerSecurityDefinitionModel[] securityArray = security as SwaggerSecurityDefinitionModel[] ?? security.ToArray();
            if (securityArray.Length == 0)
                return;

            foreach (SwaggerSecurityDefinitionModel securityDefinition in securityArray)
            {
                Enum.TryParse(securityDefinition.Type, true, out SecuritySchemeType type);
                Enum.TryParse(securityDefinition.In, true, out ParameterLocation location);

                opts.AddSecurityDefinition(securityDefinition.DisplayName, new OpenApiSecurityScheme
                {
                    Name = securityDefinition.Name,
                    Description = securityDefinition.Description,
                    Type = type,
                    Scheme = securityDefinition.Scheme,
                    In = location
                });
            }
            opts.OperationFilter<SecurityRequirementsOperationFilter>();
        }

        internal static void AddSwaggerStsSecurity(this SwaggerGenOptions opts, SwaggerStsSecurityModel security)
        {
            if (security == null) return;
            Dictionary<string, string> scopesDictionary = security.Scopes.Aggregate(new Dictionary<string, string>(), (agg, curr) =>
            {
                agg.Add(curr, string.Empty);
                return agg;
            });
            opts.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri($"{security.StsUrl}/connect/authorize"),
                        TokenUrl = new Uri($"{security.StsUrl}/connect/token"),
                        Scopes = scopesDictionary
                    }
                }
            });
            opts.OperationFilter<AuthorizeCheckOperationFilter>(security.Scopes.ToList());
        }
    }
}
