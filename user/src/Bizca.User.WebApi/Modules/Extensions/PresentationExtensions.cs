namespace Bizca.User.WebApi.Modules.Extensions
{
    using Bizca.User.Application.UseCases.ConfirmChannelCode;
    using Bizca.User.Application.UseCases.CreateUser;
    using Bizca.User.Application.UseCases.GetUser.Detail;
    using Bizca.User.Application.UseCases.GetUser.List;
    using Bizca.User.Application.UseCases.RegisterCodeConfirmation;
    using Bizca.User.Application.UseCases.UpdateUser;
    using Bizca.User.WebApi.UseCases.V1.ConfirmChannelCode;
    using Bizca.User.WebApi.UseCases.V1.CreateUser;
    using Bizca.User.WebApi.UseCases.V1.GetUserDetails;
    using Bizca.User.WebApi.UseCases.V1.GetUsers;
    using Bizca.User.WebApi.UseCases.V1.RegisterConfirmationCode;
    using Bizca.User.WebApi.UseCases.V1.UpdateUser;
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
                    .AddScoped<IGetUserDetailOutput>(x => x.GetRequiredService<GetUserDetailPresenter>())
                    .AddScoped<CreateUserPresenter>()
                    .AddScoped<ICreateUserOutput>(x => x.GetRequiredService<CreateUserPresenter>())
                    .AddScoped<UpdateUserPresenter>()
                    .AddScoped<IUpdateUserOutput>(x => x.GetRequiredService<UpdateUserPresenter>())
                    .AddScoped<RegisterCodeConfirmationPresenter>()
                    .AddScoped<IRegisterCodeConfirmationOutput>(x => x.GetRequiredService<RegisterCodeConfirmationPresenter>())
                    .AddScoped<ConfirmChannelCodePresenter>()
                    .AddScoped<IConfirmChannelCodeOutput>(x => x.GetRequiredService<ConfirmChannelCodePresenter>());

            return services;
        }
    }
}
