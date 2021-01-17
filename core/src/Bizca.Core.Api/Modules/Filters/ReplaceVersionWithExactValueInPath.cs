namespace Bizca.Core.Api.Modules.Filters
{
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using System.Linq;

    public sealed class ReplaceVersionWithExactValueInPath : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Paths = swaggerDoc.Paths.Aggregate(new OpenApiPaths(),
                (acc, curr) =>
                {
                    acc.Add(curr.Key.Replace("v{version}", swaggerDoc.Info.Version), curr.Value);
                    return acc;
                });
        }
    }
}