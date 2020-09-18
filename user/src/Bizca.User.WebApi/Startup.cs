namespace Bizca.User.WebApi
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        #region fields & ctor

        private readonly IHostEnvironment _env;
        public Startup(IHostEnvironment env)
        {
            _env = env;
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }

        #endregion

        public void Configure(IApplicationBuilder app)
        {
            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(
                endpoints => endpoints.MapGet("/", async context => await context.Response.WriteAsync("Hello World!")
                                .ConfigureAwait(false)));
        }
    }

}