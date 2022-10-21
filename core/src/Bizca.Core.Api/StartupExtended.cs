namespace Bizca.Core.Api
{
    using FluentValidation;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Modules.Configuration;
    using Modules.Extensions;
    using Modules.Filters;
    using Newtonsoft.Json;

    public abstract class StartupExtended
    {
        protected readonly IConfiguration Configuration;
        protected readonly IHostEnvironment Environment;

        protected StartupExtended(IConfiguration configuration, IHostEnvironment environment)
        {
            this.Configuration = configuration;
            this.Environment = environment;
        }

        protected void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureServiceCollection(Configuration)
                .AddHttpContextAccessor()
                .AddRouting(options => options.LowercaseUrls = true)
                .AddValidatorsFromAssemblyContaining<VersioningConfigurationModelValidator>()
                .AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)))
                .AddControllersAsServices()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.UseCamelCasing(true);
                });
        }

        protected void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevEnvironment())
                app.UseDeveloperExceptionPage();

            app.ConfigureApp(Configuration)
                .UseHttpsRedirection()
                .UseCustomHttpMetrics()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}