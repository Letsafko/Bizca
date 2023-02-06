namespace Bizca.Notification.WebApi
{
    using Autofac;
    using Core.Api;
    using Core.Api.Modules.HealthChecks;
    using Core.Infrastructure.Database.Configuration;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Modules.Autofac;
    using Modules.Extensions;

    /// <summary>
    ///     Startup
    /// </summary>
    public sealed class Startup : StartupExtended
    {
        private const string DatabaseScheme = "BizcaDatabase";

        /// <summary>
        ///     Creates an instance of <see cref="Startup" />
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
        public new void ConfigureServices(IServiceCollection services)
        {
            services.Configure<DatabaseConfiguration>(Configuration.GetSection(DatabaseScheme));
            base.ConfigureServices(services);
            services.ConfigureHealthChecks()
                .AddControllers();
        }

        /// <summary>
        ///     Configures application builder.
        /// </summary>
        /// <param name="app">application builder.</param>
        public new void Configure(IApplicationBuilder app)
        {
            base.Configure(app);
            app.UseHealthChecks();
        }

        /// <summary>
        ///     Configures autofac container.
        /// </summary>
        /// <param name="builder"></param>
        public new void ConfigureContainer(ContainerBuilder builder)
        {
            StartupExtended.ConfigureContainer(builder);
            builder.RegisterModule(new InfrastructureModule());
            builder.RegisterModule(new WebApiModule());
            builder.RegisterModule(new DomainModule());
        }
    }
}