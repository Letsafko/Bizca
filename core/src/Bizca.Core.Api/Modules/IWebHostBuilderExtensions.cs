namespace Bizca.Core.Api.Modules
{
    using Microsoft.AspNetCore.Hosting;
    using System.Reflection;

    public static class IWebHostBuilderExtensions
    {
        /// <summary>
        ///     Defines custom startup
        /// </summary>
        /// <typeparam name="TStartup">startup</typeparam>
        /// <param name="builder">builder</param>
        public static IWebHostBuilder UseCustomStartup<TStartup>(this IWebHostBuilder builder) where TStartup : class
        {
            builder.UseStartup<TStartup>();
            builder.UseSetting(WebHostDefaults.ApplicationKey, Assembly.GetEntryAssembly().GetName().FullName);
            return builder;
        }
    }
}
