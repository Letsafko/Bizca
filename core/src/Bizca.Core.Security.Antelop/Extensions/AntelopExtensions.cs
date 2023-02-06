namespace Bizca.Core.Security.Antelop.Extensions
{
    using Configuratin;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Middleware;

    /// <summary>
    ///     Antelop extension methods
    /// </summary>
    public static class AntelopExtensions
    {
        /// <summary>
        ///     Adds the antelop.
        /// </summary>
        /// <param name="services">services</param>
        /// <param name="certificate">certificate</param>
        public static IServiceCollection AddAntelop(this IServiceCollection services,
            string certificate)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(Constants.AntelopScheme, _ =>
                {
                });

            services.Configure<AntelopConfiguration>(configuration => configuration.AntelopCertificate = certificate);
            services.AddSingleton<ITokenValidator, AntelopTokenValidator>();
            services.AddSingleton<IPostConfigureOptions<JwtBearerOptions>, AntelopPostConfigureOptions>();
            services.AddSingleton<AspNetAntelopSecurityTokenValidator>();

            return services;
        }
    }
}