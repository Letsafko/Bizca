namespace Bizca.Core.Api.Modules.Filters
{
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using System.Linq;

    public sealed class RemoveVersionFromParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters.Count > 0)
            {
                OpenApiParameter versionParameter = operation.Parameters.Single(p => p.Name.Equals("version", System.StringComparison.OrdinalIgnoreCase));
                operation.Parameters.Remove(versionParameter);
            }
        }
    }
}
