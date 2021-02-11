namespace Bizca.Core.Api
{
    using Bizca.Core.Api.Modules.Extensions;
    using Bizca.Core.Api.Modules.Filters;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public abstract class StartupExtended
    {
        protected readonly IHostEnvironment environment;
        protected readonly IConfiguration configuration;
        protected StartupExtended(IConfiguration configuration, IHostEnvironment environment)
        {
            this.environment = environment;
            this.configuration = configuration;
        }

        protected void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureServiceCollection(configuration)
                    .AddHttpContextAccessor()
                    .AddRouting(options => options.LowercaseUrls = true)
                    .AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));
        }

        protected void Configure(IApplicationBuilder app)
        {
            if (environment.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.ConfigureApp(configuration)
               .UseHttpsRedirection()
               .ConfigureSwagger(configuration.GetSwaggerConfiguration())
               .UseCustomHttpMetrics();
        }
    }
}