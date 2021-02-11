namespace Bizca.Gateway
{
    using Bizca.Core.Api.Modules.Extensions;
    using Bizca.Gateway.Application.Extensions;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using System.Diagnostics.CodeAnalysis;

    public static class Program
    {
        [ExcludeFromCodeCoverage]
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        [ExcludeFromCodeCoverage]
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                       .ConfigureWebHostDefaults(webBuilder =>
                       {
                           webBuilder.UseIISIntegration();
                           webBuilder.UseStartup<Startup>();
                       })
                       .ConfigureAppConfiguration((context, config) =>
                       {
                           config.AddJsonFile("appsettings.json", false, true);
                           config.AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);

                           if (context.HostingEnvironment.IsDevelopment())
                           {
                               config.AddJsonFile("ocelot.global.json", optional: false, reloadOnChange: true);
                               config.AddJsonFile("ocelot.Development.json", optional: false, reloadOnChange: true);
                               config.AddJsonFile("cache.development.json", optional: true, reloadOnChange: true);
                           }
                           else
                           {
                               config.AddJsonFile("cache.json", optional: true, reloadOnChange: true);
                               config.MergeOcelotFiles();
                           }

                           config.AddAzureKeyVaultWithEnvVariables();
                       })
                       .ConfigureLogging((context, builder) =>
                       {
                           //builder.AddApplicationInsights();
                           //builder.AddFilter<Microsoft.Extensions.Logging.ApplicationInsights.ApplicationInsightsLoggerProvider>
                           // ("", Enum.Parse<LogLevel>(context.Configuration["Logging:LogLevel:Default"]));
                       });
        }
    }
}