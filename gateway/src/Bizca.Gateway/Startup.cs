namespace Bizca.Gateway
{
    using Application.Extensions;
    using Core.Api.Modules.Extensions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System.Diagnostics.CodeAnalysis;

    public class Startup
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Startup" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        ///     Gets the appsettings configuration.
        /// </summary>
        /// <value>The configuration.</value>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureServiceCollection(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [ExcludeFromCodeCoverage]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevEnvironment()) app.UseDeveloperExceptionPage();

            app.UseRouting();
            app.UseStaticFiles();
            //app.UseHealthChecks(PartnerConfiguration);
            app.ConfigureApp(Configuration);
        }
    }
}