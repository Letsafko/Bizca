namespace Bizca.Bff.Functional.Test
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.Extensions.Configuration;
    using WebApi;

    public class BffWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((context, cb) =>
            {
                cb.AddJsonFile("appsettings.json", false);
            });
            base.ConfigureWebHost(builder);
        }
    }
}