namespace Bizca.Core.Api.Modules.Extensions
{
    using Microsoft.AspNetCore.Hosting;
    using System.Reflection;

    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder UseCustomStartup<TStartup>(this IWebHostBuilder builder) where TStartup : class
        {
            builder.UseStartup<TStartup>();
            builder.UseSetting(WebHostDefaults.ApplicationKey, Assembly.GetEntryAssembly()?.GetName().FullName);
            return builder;
        }
    }
}