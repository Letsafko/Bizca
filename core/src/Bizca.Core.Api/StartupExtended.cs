namespace Bizca.Core.Api
{
    using Autofac;
    using FluentValidation;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Modules.Autofac;
    using Modules.Configuration;
    using Modules.Extensions;
    using Modules.Filters;
    using Newtonsoft.Json;

    public abstract class StartupExtended
    {
        protected IConfiguration Configuration { get; }
        private readonly IHostEnvironment _environment;

        protected StartupExtended(IConfiguration configuration, IHostEnvironment environment)
        {
            Configuration = configuration;
            _environment = environment;
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
            if (_environment.IsDevEnvironment())
                app.UseDeveloperExceptionPage();

            app.ConfigureApp(Configuration)
                .UseHttpsRedirection()
                .UseCustomHttpMetrics()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints => endpoints.MapControllers());
        }
    
        protected static void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new InfrastructureModule());
            builder.RegisterModule(new WebApiModule());
            builder.RegisterModule(new DomainModule());
        }
    }
}