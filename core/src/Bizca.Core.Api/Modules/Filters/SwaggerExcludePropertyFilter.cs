namespace Bizca.Core.Api.Modules.Filters
{
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class SwaggerExcludeAttribute : Attribute
    {
    }

    public class SwaggerExcludePropertyFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            List<PropertyInfo> parameters =
                context
                    .ApiDescription
                    .ActionDescriptor
                    ?.Parameters
                    ?.SelectMany(x => x.ParameterType.GetProperties())
                    ?.Where(x => x.GetCustomAttribute<SwaggerExcludeAttribute>() != null)
                    ?.ToList();

            foreach (PropertyInfo param in parameters)
            {
                var openApiParameter = operation
                    .Parameters
                    .SingleOrDefault(x 
                        => param.Name.Equals(x.Name, StringComparison.OrdinalIgnoreCase));
                
                if (openApiParameter is null)
                    continue;

                operation.Parameters.Remove(openApiParameter);
            }
        }
    }
}