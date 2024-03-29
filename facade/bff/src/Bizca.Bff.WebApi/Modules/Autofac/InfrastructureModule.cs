﻿namespace Bizca.Bff.WebApi.Modules.Autofac
{
    using Bizca.Bff.Domain.Entities.Subscription;
    using Bizca.Bff.Domain.Entities.User;
    using Bizca.Bff.Domain.Referentials.Bundle;
    using Bizca.Bff.Domain.Referentials.Procedure;
    using Bizca.Bff.Domain.Wrappers.Notification;
    using Bizca.Bff.Domain.Wrappers.Users;
    using Bizca.Bff.Infrastructure.Cache;
    using Bizca.Bff.Infrastructure.Persistance;
    using Bizca.Bff.Infrastructure.Wrappers.Notifications;
    using Bizca.Bff.Infrastructure.Wrappers.Users;
    using Bizca.Core.Domain;
    using Bizca.Core.Domain.EmailTemplate;
    using Bizca.Core.Infrastructure;
    using Bizca.Core.Infrastructure.Cache;
    using Bizca.Core.Infrastructure.Database;
    using Bizca.Core.Infrastructure.Persistance;
    using global::Autofac;

    /// <summary>
    ///     Infrastrcuture modules.
    /// </summary>
    public sealed class InfrastructureModule : Module
    {
        /// <summary>
        ///     Load services.
        /// </summary>
        /// <param name="builder">container builder.</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConnectionFactory>().As<IConnectionFactory>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            LoadRepositories(builder);
            LoadWrappers(builder);
        }

        private void LoadRepositories(ContainerBuilder builder)
        {
            builder.RegisterType<SubscriptionRepository>().As<ISubscriptionRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();

            builder.RegisterType<ProcedureRepository>().As<IProcedureRepository>().InstancePerLifetimeScope();
            builder.RegisterDecorator<CacheProcedureRepository, IProcedureRepository>();

            builder.RegisterType<BundleRepository>().As<IBundleRepository>().InstancePerLifetimeScope();
            builder.RegisterDecorator<CacheBundleRepository, IBundleRepository>();

            builder.RegisterType<EmailTemplateRepository>().As<IEmailTemplateRepository>().InstancePerLifetimeScope();
            builder.RegisterDecorator<CacheEmailTemplateRepository, IEmailTemplateRepository>();
        }
        private void LoadWrappers(ContainerBuilder builder)
        {
            builder.RegisterType<NotificationWrapper>().As<INotificationWrapper>();
            builder.RegisterType<UserWrapper>().As<IUserWrapper>();
        }
    }
}