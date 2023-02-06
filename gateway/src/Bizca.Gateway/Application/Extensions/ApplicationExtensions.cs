namespace Bizca.Gateway.Application.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Microsoft.OpenApi;
    using Microsoft.OpenApi.Extensions;
    using Microsoft.OpenApi.Models;
    using Microsoft.OpenApi.Readers;
    using MMLib.SwaggerForOcelot.Configuration;
    using Ocelot.Middleware;
    using Swashbuckle.AspNetCore.SwaggerUI;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>Application builder extension methods</summary>
    public static class ApplicationExtensions
    {
        private const string SwaggerDocumentationPath = "/swagger/docs/v1.0/migrationProxy";

        /// <summary>Configures the application.</summary>
        /// <param name="app">The application.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        [ExcludeFromCodeCoverage]
        public static IApplicationBuilder ConfigureApp(this IApplicationBuilder app, IConfiguration configuration)
        {
            List<SwaggerEndPointOptions> swaggerEndpoints =
                app.ApplicationServices.GetService<IOptions<List<SwaggerEndPointOptions>>>().Value;

            if (swaggerEndpoints.Count > 0)
                app.UseSwaggerForOcelotUI(setup =>
                    {
                        setup.ConfigObject.DefaultModelExpandDepth = 2;
                        setup.ConfigObject.DefaultModelRendering = ModelRendering.Model;
                        setup.ConfigObject.DocExpansion = DocExpansion.None;
                        setup.ConfigObject.DeepLinking = true;
                        setup.ConfigObject.DisplayOperationId = true;
                        setup.InjectStylesheet("/swagger/custom-swagger.css");
                        setup.ReConfigureUpstreamSwaggerJson = (context, json) =>
                            ReConfigureUpstreamSwaggerJson(json, configuration);
                    })
                    .UseReDoc(c =>
                    {
                        c.NoAutoAuth();
                        c.DocumentTitle = "Xpollens API Documentation";
                        c.RoutePrefix = "documentation";
                        c.InjectStylesheet("/swagger/custom.css");
                        c.SpecUrl = SwaggerDocumentationPath;
                    });

            //Add isAlive endpoint to check is Ocelot instance is up
            app.UseOcelot(pipelineConfiguration =>
            {
                pipelineConfiguration.PreErrorResponderMiddleware = async (ctx, next) =>
                {
                    if (ctx.Request.Path.Equals(new PathString("/isalive")))
                        await ctx.Response.WriteAsync("ok");
                    else
                        await next.Invoke();
                };
            });

            return app;
        }

        [ExcludeFromCodeCoverage]
        private static string ReConfigureUpstreamSwaggerJson(string swaggerJson, IConfiguration configuration)
        {
            OpenApiDocument document = new OpenApiStringReader().Read(swaggerJson, out OpenApiDiagnostic diagnostic);

            document.ExcludePartnerCode()
                .AddOpenApiSecurityScheme(configuration)
                .AddOpenApiOAuthFlow(configuration);

            return document.Serialize(OpenApiSpecVersion.OpenApi3_0, OpenApiFormat.Json);
        }
    }
}