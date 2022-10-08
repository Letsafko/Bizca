namespace Bizca.Bff.WebApi.Modules.Autofac
{
    using Core.Domain.Referential.Model;
    using Core.Domain.Referential.Repository;
    using Core.Domain.Referential.Services;
    using Core.Infrastructure;
    using Core.Infrastructure.Database;
    using Core.Infrastructure.Persistence;
    using Core.Infrastructure.Persistence.RepositoryCache;
    using Domain.Entities.Subscription;
    using Domain.Entities.User;
    using Domain.Provider.ContactList;
    using Domain.Provider.Folder;
    using Domain.Referentials.Bundle;
    using Domain.Referentials.Procedure;
    using Domain.Wrappers.Contact;
    using Domain.Wrappers.Notification;
    using Domain.Wrappers.Users;
    using global::Autofac;
    using Infrastructure.Cache;
    using Infrastructure.Persistance;
    using Infrastructure.Wrappers.Contact;
    using Infrastructure.Wrappers.Notifications;
    using Infrastructure.Wrappers.Users;

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

            builder.RegisterType<ContactListRepository>().As<IContactListRepository>().InstancePerLifetimeScope();
            builder.RegisterDecorator<CacheContactListRepository, IContactListRepository>();

            builder.RegisterType<FolderRepository>().As<IFolderRepository>().InstancePerLifetimeScope();
            builder.RegisterDecorator<CacheFolderRepository, IFolderRepository>();

            builder.RegisterType<EconomicActivityRepository>().As<IEconomicActivityRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterDecorator<CacheEconomicActivityRepository, IEconomicActivityRepository>();

            builder.RegisterType<CivilityRepository>().As<ICivilityRepository>().InstancePerLifetimeScope();
            builder.RegisterDecorator<CacheCivilityRepository, ICivilityRepository>();

            builder.RegisterType<PartnerRepository>().As<IPartnerRepository>().InstancePerLifetimeScope();
            builder.RegisterDecorator<CachePartnerRepository, IPartnerRepository>();

            builder.RegisterType<CountryRepository>().As<ICountryRepository>().InstancePerLifetimeScope();
            builder.RegisterDecorator<CacheCountryRepository, ICountryRepository>();

            builder.RegisterType<ReferentialService>().As<IReferentialService>().InstancePerLifetimeScope();
        }

        private void LoadWrappers(ContainerBuilder builder)
        {
            builder.RegisterType<NotificationWrapper>().As<INotificationWrapper>();
            builder.RegisterType<ContactWrapper>().As<IContactWrapper>();
            builder.RegisterType<UserWrapper>().As<IUserWrapper>();
        }
    }
}