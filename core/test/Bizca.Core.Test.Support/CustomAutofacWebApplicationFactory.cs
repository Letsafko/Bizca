namespace Bizca.Core.Test.Support
{
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.Extensions.Hosting;

    public class CustomAutofacWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder
                .UseEnvironment("Test")
                .UseServiceProviderFactory(new CustomAutofacServiceProviderFactory());

            return base.CreateHost(builder);
        }
    }
}