namespace Bizca.User.WebApi
{
    using Autofac;
    using Bizca.Core.Api;
    using Bizca.Core.Infrastructure.Abstracts.Configuration;
    using Bizca.User.WebApi.Modules;
    using Bizca.User.WebApi.Modules.Extensions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    ///     Startup
    /// </summary>
    public sealed class Startup : StartupExtended
    {
        /// <summary>
        ///     Creates an instance of <see cref="Startup"/>
        /// </summary>
        /// <param name="environment"></param>
        /// <param name="configuration"></param>
        public Startup(IHostEnvironment environment, IConfiguration configuration) : base(configuration, environment)
        {
        }

        /// <summary>
        ///     Configures services.
        /// </summary>
        /// <param name="services">service collection.</param>
        new public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<BizcaDatabaseConfiguration>(_configuration.GetSection("BizcaDatabase"));
            base.ConfigureServices(services);
            services.AddPresentersV1()
                    .AddControllers()
                    .AddNewtonsoftJson(options => options.UseCamelCasing(true));
        }

        /// <summary>
        ///     Configures application builder.
        /// </summary>
        /// <param name="app">application builder.</param>
        new public void Configure(IApplicationBuilder app)
        {
            base.Configure(app);
            app.UseRouting()
               .UseAuthentication()
               .UseAuthorization()
               .UseEndpoints(endpoints => endpoints.MapControllers());
        }

        /// <summary>
        ///     Configures autofac container.
        /// </summary>
        /// <param name="builder"></param>
        public static void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new DomainModule());
            builder.RegisterModule(new MediatorModule());
            builder.RegisterModule(new InfrastructureModule());
            builder.RegisterModule(new ApplicationModule());
        }
    }
}