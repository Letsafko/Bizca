namespace Bizca.Gateway
{
    using Application.Extensions;
    using Core.Api.Modules.Extensions;
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
                    config.AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", true, true);

                    if (context.HostingEnvironment.IsDevEnvironment())
                    {
                        config.AddJsonFile("ocelot.global.json", false, true);
                        config.AddJsonFile("ocelot.Development.json", false, true);
                        config.AddJsonFile("cache.development.json", true, true);
                    }
                    else
                    {
                        config.AddJsonFile("cache.json", true, true);
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