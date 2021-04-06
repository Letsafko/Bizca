namespace Bizca.Notification.WebApi
{
    using Autofac;
    using Bizca.Core.Api;
    using Bizca.Core.Api.Modules.HealthChecks;
    using Bizca.Core.Infrastructure.Database.Configuration;
    using Bizca.Notification.WebApi.Modules.Autofac;
    using Bizca.Notification.WebApi.Modules.Extensions;
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

        private const string DatabaseScheme = "BizcaDatabase";

        /// <summary>
        ///     Configures services.
        /// </summary>
        /// <param name="services">service collection.</param>
        new public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<DatabaseConfiguration>(configuration.GetSection(DatabaseScheme));
            base.ConfigureServices(services);
            services.ConfigureHealthChecks()
                    .AddControllers();
        }

        /// <summary>
        ///     Configures application builder.
        /// </summary>
        /// <param name="app">application builder.</param>
        new public void Configure(IApplicationBuilder app)
        {
            base.Configure(app);
            app.UseHealthChecks();
        }

        /// <summary>
        ///     Configures autofac container.
        /// </summary>
        /// <param name="builder"></param>
        public static void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new InfrastructureModule());
            builder.RegisterModule(new ApplicationModule());
            builder.RegisterModule(new MediatorModule());
            builder.RegisterModule(new WebApiModule());
            builder.RegisterModule(new DomainModule());
        }
    }
}