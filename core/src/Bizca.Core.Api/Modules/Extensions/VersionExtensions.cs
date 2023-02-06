namespace Bizca.Core.Api.Modules.Extensions
{
    using Configuration;
    using Conventions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Versioning;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    public static class VersionExtensions
    {
        public static void AddVersioning(this IServiceCollection services,
            VersioningConfigurationModel versioningConfiguration)
        {
            void MvcOptions(MvcOptions x)
            {
                x.Conventions.Add(new DefaultApiVersionConvention(versioningConfiguration.RouteConstraintName));
            }

            services.Configure((Action<MvcOptions>)MvcOptions);
            services.AddApiVersioning(opts =>
            {
                opts.DefaultApiVersion = ApiVersion.Parse(versioningConfiguration.Default);
                opts.AssumeDefaultVersionWhenUnspecified = true;
                opts.ApiVersionReader = new UrlSegmentApiVersionReader();
                opts.RouteConstraintName = versioningConfiguration.RouteConstraintName;
            });
        }
    }
}