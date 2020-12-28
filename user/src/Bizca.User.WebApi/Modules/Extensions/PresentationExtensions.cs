namespace Bizca.User.WebApi.Modules.Extensions
{
    using Bizca.User.Application.UseCases.GetUser.Detail;
    using Bizca.User.Application.UseCases.GetUser.List;
    using Bizca.User.WebApi.UseCases.V1.GetUserDetails;
    using Bizca.User.WebApi.UseCases.V1.GetUsers;
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
            services.AddScoped<GetUsersPresenter>()
                    .AddScoped<IGetUsersOutput>(x => x.GetRequiredService<GetUsersPresenter>())
                    .AddScoped<GetUserDetailPresenter>()
                    .AddScoped<IGetUserDetailOutput>(x => x.GetRequiredService<GetUserDetailPresenter>());

            return services;
        }
    }
}
