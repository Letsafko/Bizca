namespace Bizca.Bff.WebApi.Modules.Autofac
{
    using Bizca.Bff.Application.UseCases.AuthenticateUser;
    using Bizca.Bff.Application.UseCases.ConfirmChannelCode;
    using Bizca.Bff.Application.UseCases.CreateNewUser;
    using Bizca.Bff.Application.UseCases.CreateSubscription;
    using Bizca.Bff.Application.UseCases.GetBundles;
    using Bizca.Bff.Application.UseCases.GetProcedures;
    using Bizca.Bff.Application.UseCases.GetUserDetails;
    using Bizca.Bff.Application.UseCases.GetUsers;
    using Bizca.Bff.Application.UseCases.GetUserSubscriptionDetails;
    using Bizca.Bff.Application.UseCases.GetUserSubscriptions;
    using Bizca.Bff.Application.UseCases.RegisterSmsCodeConfirmation;
    using Bizca.Bff.Application.UseCases.ReInitializedPassword;
    using Bizca.Bff.Application.UseCases.SubscriptionActivation;
    using Bizca.Bff.Application.UseCases.UpdateSubscription;
    using Bizca.Bff.Application.UseCases.UpdateUser;
    using Bizca.Bff.Application.UseCases.UpsertPassword;
    using Bizca.Bff.WebApi.UseCases.V10.AuthenticateUser;
    using Bizca.Bff.WebApi.UseCases.V10.ConfirmChannelCode;
    using Bizca.Bff.WebApi.UseCases.V10.CreateNewUser;
    using Bizca.Bff.WebApi.UseCases.V10.CreateSubscription;
    using Bizca.Bff.WebApi.UseCases.V10.GetBundles;
    using Bizca.Bff.WebApi.UseCases.V10.GetProcedures;
    using Bizca.Bff.WebApi.UseCases.V10.GetUserDetails;
    using Bizca.Bff.WebApi.UseCases.V10.GetUsers;
    using Bizca.Bff.WebApi.UseCases.V10.GetUserSubscriptionDetails;
    using Bizca.Bff.WebApi.UseCases.V10.GetUserSubscriptions;
    using Bizca.Bff.WebApi.UseCases.V10.RegisterSmsCodeConfirmation;
    using Bizca.Bff.WebApi.UseCases.V10.ReInitializedPassword;
    using Bizca.Bff.WebApi.UseCases.V10.SubscriptionActivation;
    using Bizca.Bff.WebApi.UseCases.V10.UpdateSubscription;
    using Bizca.Bff.WebApi.UseCases.V10.UpdateUser;
    using Bizca.Bff.WebApi.UseCases.V10.UpsertPassword;
    using global::Autofac;

    /// <summary>
    ///     WebApi modules.
    /// </summary>
    public sealed class WebApiModule : Module
    {
        /// <summary>
        ///     Load services.
        /// </summary>
        /// <param name="builder">container builder.</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RegisterSmsCodeConfirmationPresenter>().InstancePerLifetimeScope();
            builder.Register<IRegisterSmsCodeConfirmationOutput>(x => x.Resolve<RegisterSmsCodeConfirmationPresenter>()).InstancePerLifetimeScope();

            builder.RegisterType<GetUserDetailsPresenter>().InstancePerLifetimeScope();
            builder.Register<IGetUserDetailsOutput>(x => x.Resolve<GetUserDetailsPresenter>()).InstancePerLifetimeScope();

            builder.RegisterType<ReInitializedPasswordPresenter>().InstancePerLifetimeScope();
            builder.Register<IReInitializedPasswordOutput>(x => x.Resolve<ReInitializedPasswordPresenter>()).InstancePerLifetimeScope();

            builder.RegisterType<SubscriptionActivationPresenter>().InstancePerLifetimeScope();
            builder.Register<ISubscriptionActivationOutput>(x => x.Resolve<SubscriptionActivationPresenter>()).InstancePerLifetimeScope();

            builder.RegisterType<UpsertPasswordPresenter>().InstancePerLifetimeScope();
            builder.Register<IUpsertPasswordOutput>(x => x.Resolve<UpsertPasswordPresenter>()).InstancePerLifetimeScope();

            builder.RegisterType<GetUsersPresenter>().InstancePerLifetimeScope();
            builder.Register<IGetUsersOutput>(x => x.Resolve<GetUsersPresenter>()).InstancePerLifetimeScope();

            builder.RegisterType<UpdateUserPresenter>().InstancePerLifetimeScope();
            builder.Register<IUpdateUserOutput>(x => x.Resolve<UpdateUserPresenter>()).InstancePerLifetimeScope();

            builder.RegisterType<GetBundlesPresenter>().InstancePerLifetimeScope();
            builder.Register<IGetBundlesOutput>(x => x.Resolve<GetBundlesPresenter>()).InstancePerLifetimeScope();

            builder.RegisterType<AuthenticateUserPresenter>().InstancePerLifetimeScope();
            builder.Register<IAuthenticateUserOutput>(x => x.Resolve<AuthenticateUserPresenter>()).InstancePerLifetimeScope();

            builder.RegisterType<CreateNewUserPresenter>().InstancePerLifetimeScope();
            builder.Register<ICreateNewUserOutput>(x => x.Resolve<CreateNewUserPresenter>()).InstancePerLifetimeScope();

            builder.RegisterType<ConfirmChannelCodePresenter>().InstancePerLifetimeScope();
            builder.Register<IConfirmChannelCodeOutput>(x => x.Resolve<ConfirmChannelCodePresenter>()).InstancePerLifetimeScope();

            builder.RegisterType<GetProceduresPresenter>().InstancePerLifetimeScope();
            builder.Register<IGetProceduresOutput>(x => x.Resolve<GetProceduresPresenter>()).InstancePerLifetimeScope();

            builder.RegisterType<CreateSubscriptionPresenter>().InstancePerLifetimeScope();
            builder.Register<ICreateSubscriptionOutput>(x => x.Resolve<CreateSubscriptionPresenter>()).InstancePerLifetimeScope();

            builder.RegisterType<UpdateSubscriptionPresenter>().InstancePerLifetimeScope();
            builder.Register<IUpdateSubscriptionOutput>(x => x.Resolve<UpdateSubscriptionPresenter>()).InstancePerLifetimeScope();

            builder.RegisterType<GetUserSubscriptionDetailsPresenter>().InstancePerLifetimeScope();
            builder.Register<IGetUserSubscriptionDetailsOutput>(x => x.Resolve<GetUserSubscriptionDetailsPresenter>()).InstancePerLifetimeScope();

            builder.RegisterType<GetUserSubscriptionsPresenter>().InstancePerLifetimeScope();
            builder.Register<IGetUserSubscriptionsOutput>(x => x.Resolve<GetUserSubscriptionsPresenter>()).InstancePerLifetimeScope();
        }
    }
}