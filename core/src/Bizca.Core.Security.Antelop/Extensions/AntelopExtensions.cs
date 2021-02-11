namespace Bizca.Core.Security.Antelop.Extensions
{
    using Bizca.Core.Security.Antelop.Configuratin;
    using Bizca.Core.Security.Antelop.Middleware;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

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
                .AddJwtBearer(Constants.ANTELOP_SCHEME, _ =>
                {
                });

            services.Configure<AntelopConfiguration>((configuration) => configuration.AntelopCertificate = certificate);
            services.AddSingleton<ITokenValidator, AntelopTokenValidator>();
            services.AddSingleton<IPostConfigureOptions<JwtBearerOptions>, AntelopPostConfigureOptions>();
            services.AddSingleton<AspNetAntelopSecurityTokenValidator>();

            return services;
        }
    }
}