namespace Bizca.Notification.WebApi
{
    using Autofac.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using System.Reflection;

    /// <summary>
    ///  Program.
    /// </summary>
    public static class Program
    {
        /// <summary>
        ///     Application entrance.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        ///     Configures host builder.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
                .AddAppSettingConfigurationFile()
                .UseDefaultServiceProvider((context, options) =>
                {
                    options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
                    options.ValidateOnBuild = true;
                })
                .UseServiceProviderFactory(new AutofacServiceProviderFactory());
        }

        /// <summary>
        ///     Add configuration settings.
        /// </summary>
        /// <param name="builder"></param>
        public static IHostBuilder AddAppSettingConfigurationFile(this IHostBuilder builder)
        {
            return builder.ConfigureAppConfiguration(ConfigureAppSettings);
        }

        /// <summary>
        ///     Configure settings.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="configBuilder"></param>
        private static void ConfigureAppSettings(HostBuilderContext context, IConfigurationBuilder configBuilder)
        {
            configBuilder
                .AddEnvironmentVariables()
                .AddUserSecrets(Assembly.GetEntryAssembly())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
        }
    }
}