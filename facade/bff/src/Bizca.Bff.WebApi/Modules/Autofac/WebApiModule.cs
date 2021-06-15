﻿namespace Bizca.Bff.WebApi.Modules.Autofac
{
    using Bizca.Bff.Application.UseCases.CreateNewUser;
    using Bizca.Bff.Application.UseCases.CreateSubscription;
    using Bizca.Bff.Application.UseCases.GetUserSubscriptionDetails;
    using Bizca.Bff.Application.UseCases.GetUserSubscriptions;
    using Bizca.Bff.Application.UseCases.UpdateSubscription;
    using Bizca.Bff.WebApi.UseCases.V10.CreateNewUser;
    using Bizca.Bff.WebApi.UseCases.V10.CreateSubscription;
    using Bizca.Bff.WebApi.UseCases.V10.GetUserSubscriptionDetails;
    using Bizca.Bff.WebApi.UseCases.V10.GetUserSubscriptions;
    using Bizca.Bff.WebApi.UseCases.V10.UpdateSubscription;
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
            builder.RegisterType<CreateNewUserPresenter>().InstancePerLifetimeScope();
            builder.Register<ICreateNewUserOutput>(x => x.Resolve<CreateNewUserPresenter>()).InstancePerLifetimeScope();

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