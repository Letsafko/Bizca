namespace Bizca.User.WebApi.Modules.Autofac
{
    using Bizca.User.Application.UseCases.AuthenticateUser;
    using Bizca.User.Application.UseCases.ConfirmChannelCode;
    using Bizca.User.Application.UseCases.CreateUser;
    using Bizca.User.Application.UseCases.GetUserDetail;
    using Bizca.User.Application.UseCases.RegisterCodeConfirmation;
    using Bizca.User.Application.UseCases.RegisterPassword;
    using Bizca.User.Application.UseCases.UpdateUser;
    using Bizca.User.WebApi.UseCases.V1.AuthenticateUser;
    using Bizca.User.WebApi.UseCases.V1.ConfirmChannelCode;
    using Bizca.User.WebApi.UseCases.V1.CreateUser;
    using Bizca.User.WebApi.UseCases.V1.GetUserDetails;
    using Bizca.User.WebApi.UseCases.V1.RegisterConfirmationCode;
    using Bizca.User.WebApi.UseCases.V1.RegisterPassword;
    using Bizca.User.WebApi.UseCases.V1.UpdateUser;
    using global::Autofac;

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
            builder.Register<IAuthenticateUserOutput>(x => x.Resolve<AuthenticateUserPresenter>()).InstancePerLifetimeScope();

            builder.RegisterType<RegisterPasswordPresenter>().InstancePerLifetimeScope();
            builder.Register<IRegisterPasswordOutput>(x => x.Resolve<RegisterPasswordPresenter>()).InstancePerLifetimeScope();

            builder.RegisterType<GetUserDetailPresenter>().InstancePerLifetimeScope();
            builder.Register<IGetUserDetailOutput>(x => x.Resolve<GetUserDetailPresenter>()).InstancePerLifetimeScope();

            builder.RegisterType<CreateUserPresenter>().InstancePerLifetimeScope();
            builder.Register<ICreateUserOutput>(x => x.Resolve<CreateUserPresenter>()).InstancePerLifetimeScope();

            builder.RegisterType<UpdateUserPresenter>().InstancePerLifetimeScope();
            builder.Register<IUpdateUserOutput>(x => x.Resolve<UpdateUserPresenter>()).InstancePerLifetimeScope();

            builder.RegisterType<RegisterCodeConfirmationPresenter>().InstancePerLifetimeScope();
            builder.Register<IRegisterCodeConfirmationOutput>(x => x.Resolve<RegisterCodeConfirmationPresenter>()).InstancePerLifetimeScope();

            builder.RegisterType<ConfirmChannelCodePresenter>().InstancePerLifetimeScope();
            builder.Register<IConfirmChannelCodeOutput>(x => x.Resolve<ConfirmChannelCodePresenter>()).InstancePerLifetimeScope();
        }
    }
}