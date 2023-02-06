namespace Bizca.User.WebApi.Modules.Autofac
{
    using Application.UseCases.AuthenticateUser;
    using Application.UseCases.ConfirmChannelCode;
    using Application.UseCases.CreateUser;
    using Application.UseCases.GetUserDetail;
    using Application.UseCases.RegisterCodeConfirmation;
    using Application.UseCases.RegisterPassword;
    using Application.UseCases.UpdateUser;
    using global::Autofac;
    using UseCases.V1.AuthenticateUser;
    using UseCases.V1.ConfirmChannelCode;
    using UseCases.V1.CreateUser;
    using UseCases.V1.GetUserDetails;
    using UseCases.V1.RegisterCodeConfirmation;
    using UseCases.V1.RegisterPassword;
    using UseCases.V1.UpdateUser;

    /// <summary>
    ///     Web api module.
    /// </summary>
    public sealed class WebApiModule : Module
    {
        /// <summary>
        ///     Load services into container.
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthenticateUserPresenter>().InstancePerLifetimeScope();
            builder.Register<IAuthenticateUserOutput>(x => x.Resolve<AuthenticateUserPresenter>())
                .InstancePerLifetimeScope();

            builder.RegisterType<RegisterPasswordPresenter>().InstancePerLifetimeScope();
            builder.Register<IRegisterPasswordOutput>(x => x.Resolve<RegisterPasswordPresenter>())
                .InstancePerLifetimeScope();

            builder.RegisterType<GetUserDetailPresenter>().InstancePerLifetimeScope();
            builder.Register<IGetUserDetailOutput>(x => x.Resolve<GetUserDetailPresenter>()).InstancePerLifetimeScope();

            builder.RegisterType<CreateUserPresenter>().InstancePerLifetimeScope();
            builder.Register<ICreateUserOutput>(x => x.Resolve<CreateUserPresenter>()).InstancePerLifetimeScope();

            builder.RegisterType<UpdateUserPresenter>().InstancePerLifetimeScope();
            builder.Register<IUpdateUserOutput>(x => x.Resolve<UpdateUserPresenter>()).InstancePerLifetimeScope();

            builder.RegisterType<RegisterCodeConfirmationPresenter>().InstancePerLifetimeScope();
            builder.Register<IRegisterCodeConfirmationOutput>(x => x.Resolve<RegisterCodeConfirmationPresenter>())
                .InstancePerLifetimeScope();

            builder.RegisterType<ConfirmChannelCodePresenter>().InstancePerLifetimeScope();
            builder.Register<IConfirmChannelCodeOutput>(x => x.Resolve<ConfirmChannelCodePresenter>())
                .InstancePerLifetimeScope();
        }
    }
}