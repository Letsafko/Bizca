namespace Bizca.Notification.WebApi
{
    using Autofac.Extensions.DependencyInjection;
    using Bizca.Core.Api.Modules.Extensions;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;

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
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
                .AddAppSettingConfigurationFile()
                .ConfigureSerilog()
                .UseDefaultServiceProvider((context, options) =>
                {
                    options.ValidateScopes = context.HostingEnvironment.IsDevEnvironment();
                    options.ValidateOnBuild = true;
                })
                .UseServiceProviderFactory(new AutofacServiceProviderFactory());
        }
    }
}