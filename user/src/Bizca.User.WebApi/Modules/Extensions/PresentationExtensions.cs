namespace Bizca.User.WebApi.Modules.Extensions
{
    using Bizca.User.Application.UseCases.GetUserDetail;
    using Bizca.User.WebApi.UseCases.V1.GetUser;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    ///     Presentation extensions.
    /// </summary>
    public static class PresentationExtensions
    {
        /// <summary>
        ///     Register presentation services.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddPresentersV1(this IServiceCollection services)
        {
            services.AddScoped<GetUserDetailPresenter>()
                    .AddScoped<IGetUserDetailOutput>(x => x.GetRequiredService<GetUserDetailPresenter>());
            return services;
        }
    }
}
