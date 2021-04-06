namespace Bizca.Core.Api.Modules.Extensions
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using Serilog;
    using System;
    using System.IO;
    using System.Reflection;

    public static class HostBuilderExtensions
    {
        public static IHostBuilder ConfigureSerilog(this IHostBuilder builder, string loggingjsonpath = "logging.json")
        {
            return builder
                    .ConfigureAppConfiguration((_, config) => config.AddJsonFile(loggingjsonpath, optional: true, reloadOnChange: true))
                    .UseSerilog(ConfigureLogging);
        }
        public static IHostBuilder AddAppSettingConfigurationFile(this IHostBuilder builder)
        {
            return builder.ConfigureAppConfiguration(ConfigureAppSettings);
        }

        #region private helpers

        private static void ConfigureLogging(HostBuilderContext hostingContext, LoggerConfiguration loggerConfiguration)
        {
            loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            Serilog.Debugging.SelfLog.Enable(Console.Error);
        }
        private static void ConfigureAppSettings(HostBuilderContext context, IConfigurationBuilder configBuilder)
        {
            configBuilder
                .AddEnvironmentVariables()
                .AddUserSecrets(Assembly.GetEntryAssembly())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        }

        #endregion
    }
}
