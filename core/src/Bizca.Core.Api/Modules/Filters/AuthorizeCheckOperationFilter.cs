namespace Bizca.Core.Api.Modules.Filters
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using System.Collections.Generic;
    using System.Linq;

    public class AuthorizeCheckOperationFilter : IOperationFilter
    {
        private readonly IList<string> _scopes;

        public AuthorizeCheckOperationFilter(IList<string> scopes)
        {
            this._scopes = scopes;
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if(context.MethodInfo.DeclaringType is null)
                return;
            
            bool hasAuthorizeAttribute = context
                .MethodInfo.DeclaringType.GetCustomAttributes(true)
                .Union(context.MethodInfo.GetCustomAttributes(true))
                .OfType<AuthorizeAttribute>()
                .Any();

            if (!hasAuthorizeAttribute) 
                return;
            
            operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
            operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });
            var oAuthScheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
            };
            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new() { [oAuthScheme] = _scopes }
            };
        }
    }
}