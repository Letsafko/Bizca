namespace Bizca.Core.Api
{
    using Bizca.Core.Api.Modules;
    using Bizca.Core.Api.Modules.Common;
    using Bizca.Core.Api.Modules.Filters;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public abstract class StartupExtended
    {
        protected readonly IHostEnvironment _enviroment;
        protected readonly IConfiguration _configuration;
        protected StartupExtended(IConfiguration configuration, IHostEnvironment environment)
        {
            _enviroment = environment;
            _configuration = configuration;
        }

        protected void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureServiceCollection(_configuration)
                    .AddHttpContextAccessor()
                    .AddRouting(options => options.LowercaseUrls = true)
                    .AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));
        }

        protected void Configure(IApplicationBuilder app)
        {
            if (_enviroment.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.ConfigureApp(_configuration)
               .UseHttpsRedirection()
               .ConfigureSwagger(_configuration.GetSwaggerConfiguration())
               .UseCustomHttpMetrics();
        }
    }
}
