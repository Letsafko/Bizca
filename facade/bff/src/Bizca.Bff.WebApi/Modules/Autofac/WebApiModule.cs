namespace Bizca.Bff.WebApi.Modules.Autofac
{
    using Application.UseCases.AuthenticateUser;
    using Application.UseCases.ConfirmChannelCode;
    using Application.UseCases.CreateNewUser;
    using Application.UseCases.CreateSubscription;
    using Application.UseCases.FreezeSubscription;
    using Application.UseCases.GetActiveProcedures;
    using Application.UseCases.GetBundles;
    using Application.UseCases.GetProcedures;
    using Application.UseCases.GetUserDetails;
    using Application.UseCases.GetUsers;
    using Application.UseCases.GetUserSubscriptionDetails;
    using Application.UseCases.GetUserSubscriptions;
    using Application.UseCases.PaymentSubmitted;
    using Application.UseCases.RegisterSmsCodeConfirmation;
    using Application.UseCases.ReInitializedPassword;
    using Application.UseCases.SendAppointmentAvailability;
    using Application.UseCases.UpdateSubscription;
    using Application.UseCases.UpdateUser;
    using Application.UseCases.UpsertPassword;
    using global::Autofac;
    using UseCases.V10.AuthenticateUser;
    using UseCases.V10.ConfirmChannelCode;
    using UseCases.V10.CreateNewUser;
    using UseCases.V10.CreateSubscription;
    using UseCases.V10.FreezeSubscription;
    using UseCases.V10.GetActiveProcedures;
    using UseCases.V10.GetBundles;
    using UseCases.V10.GetProcedures;
    using UseCases.V10.GetUserDetails;
    using UseCases.V10.GetUsers;
    using UseCases.V10.GetUserSubscriptionDetails;
    using UseCases.V10.GetUserSubscriptions;
    using UseCases.V10.PaymentSubmitted;
    using UseCases.V10.RegisterSmsCodeConfirmation;
    using UseCases.V10.ReInitializedPassword;
    using UseCases.V10.SendProcedureAppointmentAvailability;
    using UseCases.V10.UpdateSubscription;
    using UseCases.V10.UpdateUser;
    using UseCases.V10.UpsertPassword;

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
            builder.RegisterType<PaymentSubmittedPresenter>().InstancePerLifetimeScope();
            builder.Register<IPaymentSubmittedOutput>(x => x.Resolve<PaymentSubmittedPresenter>())
                .InstancePerLifetimeScope();

            builder.RegisterType<SendProcedureAppointmentAvailabilityPresenter>().InstancePerLifetimeScope();
            builder.Register<ISendAppointmentAvailabilityOutput>(x =>
                x.Resolve<SendProcedureAppointmentAvailabilityPresenter>()).InstancePerLifetimeScope();

            builder.RegisterType<GetActiveProceduresPresenter>().InstancePerLifetimeScope();
            builder.Register<IGetActiveProceduresOutput>(x => x.Resolve<GetActiveProceduresPresenter>())
                .InstancePerLifetimeScope();

            builder.RegisterType<RegisterSmsCodeConfirmationPresenter>().InstancePerLifetimeScope();
            builder.Register<IRegisterSmsCodeConfirmationOutput>(x => x.Resolve<RegisterSmsCodeConfirmationPresenter>())
                .InstancePerLifetimeScope();

            builder.RegisterType<GetUserDetailsPresenter>().InstancePerLifetimeScope();
            builder.Register<IGetUserDetailsOutput>(x => x.Resolve<GetUserDetailsPresenter>())
                .InstancePerLifetimeScope();

            builder.RegisterType<ReInitializedPasswordPresenter>().InstancePerLifetimeScope();
            builder.Register<IReInitializedPasswordOutput>(x => x.Resolve<ReInitializedPasswordPresenter>())
                .InstancePerLifetimeScope();

            builder.RegisterType<FreezeSubscriptionPresenter>().InstancePerLifetimeScope();
            builder.Register<IFreezeSubscriptionOutput>(x => x.Resolve<FreezeSubscriptionPresenter>())
                .InstancePerLifetimeScope();

            builder.RegisterType<UpsertPasswordPresenter>().InstancePerLifetimeScope();
            builder.Register<IUpsertPasswordOutput>(x => x.Resolve<UpsertPasswordPresenter>())
                .InstancePerLifetimeScope();

            builder.RegisterType<GetUsersPresenter>().InstancePerLifetimeScope();
            builder.Register<IGetUsersOutput>(x => x.Resolve<GetUsersPresenter>()).InstancePerLifetimeScope();

            builder.RegisterType<UpdateUserPresenter>().InstancePerLifetimeScope();
            builder.Register<IUpdateUserOutput>(x => x.Resolve<UpdateUserPresenter>()).InstancePerLifetimeScope();

            builder.RegisterType<GetBundlesPresenter>().InstancePerLifetimeScope();
            builder.Register<IGetBundlesOutput>(x => x.Resolve<GetBundlesPresenter>()).InstancePerLifetimeScope();

            builder.RegisterType<AuthenticateUserPresenter>().InstancePerLifetimeScope();
            builder.Register<IAuthenticateUserOutput>(x => x.Resolve<AuthenticateUserPresenter>())
                .InstancePerLifetimeScope();

            builder.RegisterType<CreateNewUserPresenter>().InstancePerLifetimeScope();
            builder.Register<ICreateNewUserOutput>(x => x.Resolve<CreateNewUserPresenter>()).InstancePerLifetimeScope();

            builder.RegisterType<ConfirmChannelCodePresenter>().InstancePerLifetimeScope();
            builder.Register<IConfirmChannelCodeOutput>(x => x.Resolve<ConfirmChannelCodePresenter>())
                .InstancePerLifetimeScope();

            builder.RegisterType<GetProceduresPresenter>().InstancePerLifetimeScope();
            builder.Register<IGetProceduresOutput>(x => x.Resolve<GetProceduresPresenter>()).InstancePerLifetimeScope();

            builder.RegisterType<CreateSubscriptionPresenter>().InstancePerLifetimeScope();
            builder.Register<ICreateSubscriptionOutput>(x => x.Resolve<CreateSubscriptionPresenter>())
                .InstancePerLifetimeScope();

            builder.RegisterType<UpdateSubscriptionPresenter>().InstancePerLifetimeScope();
            builder.Register<IUpdateSubscriptionOutput>(x => x.Resolve<UpdateSubscriptionPresenter>())
                .InstancePerLifetimeScope();

            builder.RegisterType<GetUserSubscriptionDetailsPresenter>().InstancePerLifetimeScope();
            builder.Register<IGetUserSubscriptionDetailsOutput>(x => x.Resolve<GetUserSubscriptionDetailsPresenter>())
                .InstancePerLifetimeScope();

            builder.RegisterType<GetUserSubscriptionsPresenter>().InstancePerLifetimeScope();
            builder.Register<IGetUserSubscriptionsOutput>(x => x.Resolve<GetUserSubscriptionsPresenter>())
                .InstancePerLifetimeScope();
        }
    }
}