namespace Bizca.Bff.WebApi.Modules.Autofac
{
    using Bizca.Bff.Domain.Entities.Subscription;
    using Bizca.Bff.Domain.Entities.User;
    using Bizca.Bff.Domain.Provider.ContactList;
    using Bizca.Bff.Domain.Provider.Folder;
    using Bizca.Bff.Domain.Referentials.Bundle;
    using Bizca.Bff.Domain.Referentials.Procedure;
    using Bizca.Bff.Domain.Wrappers.Contact;
    using Bizca.Bff.Domain.Wrappers.Notification;
    using Bizca.Bff.Domain.Wrappers.Users;
    using Bizca.Bff.Infrastructure.Cache;
    using Bizca.Bff.Infrastructure.Persistance;
    using Bizca.Bff.Infrastructure.Wrappers.Contact;
    using Bizca.Bff.Infrastructure.Wrappers.Notifications;
    using Bizca.Bff.Infrastructure.Wrappers.Users;
    using Bizca.Core.Domain;
    using Bizca.Core.Infrastructure;
    using Bizca.Core.Infrastructure.Cache;
    using Bizca.Core.Infrastructure.Database;
    using Core.Domain.Referential.Model;
    using Core.Domain.Referential.Repository;
    using Core.Domain.Referential.Services;
    using Core.Infrastructure.Persistence;
    using Core.Infrastructure.Persistence.RepositoryCache;
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

            builder.RegisterType<ContactListRepository>().As<IContactListRepository>().InstancePerLifetimeScope();
            builder.RegisterDecorator<CacheContactListRepository, IContactListRepository>();

            builder.RegisterType<FolderRepository>().As<IFolderRepository>().InstancePerLifetimeScope();
            builder.RegisterDecorator<CacheFolderRepository, IFolderRepository>();

            builder.RegisterType<EconomicActivityRepository>().As<IEconomicActivityRepository>().InstancePerLifetimeScope();
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