namespace Bizca.Bff.Functional.Test
{
    using Bizca.Bff.WebApi;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.Extensions.Configuration;

    public class BffWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((context, cb) =>
            {
                cb.AddJsonFile("appsettings.json", optional: false);
            });
            base.ConfigureWebHost(builder);
        }
    }
}
