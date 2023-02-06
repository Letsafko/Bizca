namespace Bizca.Bff.WebApi.Modules.Autofac
{
    using Core.Domain.Referential.Model;
    using Core.Domain.Referential.Repository;
    using Core.Domain.Referential.Services;
    using Core.Infrastructure.Database;
    using Core.Infrastructure.Repository;
    using Core.Infrastructure.RepositoryCache;
    using Domain.Entities.Subscription;
    using Domain.Entities.User;
    using Domain.Provider.ContactList;
    using Domain.Provider.Folder;
    using Domain.Referential.Bundle;
    using Domain.Referential.Procedure;
    using Domain.Wrappers.Contact;
    using Domain.Wrappers.Notification;
    using Domain.Wrappers.Users;
    using global::Autofac;
    using Infrastructure.Cache;
    using Infrastructure.Persistence;
    using Infrastructure.Wrappers.Contact;
    using Infrastructure.Wrappers.Notifications;
    using Infrastructure.Wrappers.Users;

    /// <summary>
    ///     Infrastructure modules.
    /// </summary>
    public sealed class InfrastructureModule : Module
    {
        /// <summary>
        ///     Load services.
        /// </summary>
        /// <param name="builder">container builder.</param>
        protected override void Load(ContainerBuilder builder)
        {
            LoadRepositories(builder);
            LoadWrappers(builder);
        }

        private static void LoadRepositories(ContainerBuilder builder)
        {
            builder.RegisterType<SubscriptionRepository>().As<ISubscriptionRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();

            builder.RegisterType<ProcedureRepository>().As<IProcedureRepository>().InstancePerLifetimeScope();
            builder.RegisterDecorator<CacheProcedureRepository, IProcedureRepository>();

            builder.RegisterType<BundleRepository>().As<IBundleRepository>().InstancePerLifetimeScope();
            builder.RegisterDecorator<CacheBundleRepository, IBundleRepository>();

            builder.RegisterType<ContactListRepository>().As<IContactListRepository>().InstancePerLifetimeScope();
            builder.RegisterDecorator<CacheContactListRepository, IContactListRepository>();

            builder.RegisterType<FolderRepository>().As<IFolderRepository>().InstancePerLifetimeScope();
            builder.RegisterDecorator<CacheFolderRepository, IFolderRepository>();
        }

        private static void LoadWrappers(ContainerBuilder builder)
        {
            builder.RegisterType<NotificationWrapper>().As<INotificationWrapper>();
            builder.RegisterType<ContactWrapper>().As<IContactWrapper>();
            builder.RegisterType<UserWrapper>().As<IUserWrapper>();
        }
    }
}