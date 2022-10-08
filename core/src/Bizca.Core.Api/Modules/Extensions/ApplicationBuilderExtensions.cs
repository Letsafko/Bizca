namespace Bizca.Core.Api.Modules.Extensions
{
    using Configuration;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Swashbuckle.AspNetCore.SwaggerUI;
    using System;

    public static class ApplicationBuilderExtensions
    {
        internal static IApplicationBuilder ConfigureApp(this IApplicationBuilder app, IConfiguration configuration)
        {
            FeaturesConfigurationModel features = configuration.GetFeaturesConfiguration();
            if (features.Swagger)
                app.ConfigureSwagger(configuration.GetSwaggerConfiguration());

            if (features.Cors)
                app.ConfigureCors(configuration.GetCorsConfiguration());

            if (features.Consul)
            {
                ConsulConfigurationModel consulConfiguration = configuration.GetConsulConfiguration();
                //app.UseConsul(consulConfiguration.SystemAddress, consulConfiguration.HealthCheckEndPoint);
            }

            return app;
        }

        /// <summary>
        ///     Configures swagger.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="swaggerConfiguration">The swagger configuration.</param>
        /// <param name="specificSetupAction">The specific setup action.</param>
        public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder app,
            SwaggerConfigurationModel swaggerConfiguration, Action<SwaggerUIOptions> specificSetupAction)
        {
            app.UseSwagger();
            app.UseStaticFiles();
            app.UseSwaggerUI(c =>
            {
                foreach (VersionConfigurationModel current in swaggerConfiguration.Versions)
                    c.SwaggerEndpoint($"/swagger/v{current.Version}/swagger.json", current.Title);
                c.RoutePrefix = string.Empty;
                c.DefaultModelExpandDepth(2);
                c.DefaultModelRendering(ModelRendering.Model);
                c.DocExpansion(DocExpansion.None);
                c.EnableDeepLinking();
                c.DisplayOperationId();
                c.InjectStylesheet("custom-swagger.css");
                c.AddSwaggerStsSecurity(swaggerConfiguration.StsSecurity);
                specificSetupAction?.Invoke(c);
            });

            return app;
        }

        /// <summary>
        ///     Configures swagger.
        /// </summary>
        /// <param name="app">application builder.</param>
        /// <param name="swaggerConfiguration">swagger configuration.</param>
        public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder app,
            SwaggerConfigurationModel swaggerConfiguration)
        {
            return app.ConfigureSwagger(swaggerConfiguration, null);
        }

        /// <summary>
        ///     Configures cors.
        /// </summary>
        /// <param name="app">application builder.</param>
        /// <param name="corsConfiguration">cors configuration.</param>
        public static IApplicationBuilder ConfigureCors(this IApplicationBuilder app,
            CorsConfigurationModel corsConfiguration)
        {
            return app.UseCors(nameof(corsConfiguration.DefaultApiPolicy));
        }

        private static void AddSwaggerStsSecurity(this SwaggerUIOptions options, SwaggerStsSecurityModel security)
        {
            if (security is null)
                return;

            options.OAuthClientId(security.ClientId);
            options.OAuthClientSecret(security.ClientSecret);
            options.OAuthUsePkce();
        }
    }
}