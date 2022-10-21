namespace Bizca.User.WebApi.Modules.Autofac
{
    using Core.Domain.Referential.Model;
    using Core.Domain.Referential.Repository;
    using Core.Domain.Referential.Services;
    using Core.Infrastructure;
    using Core.Infrastructure.Database;
    using Core.Infrastructure.Persistence;
    using Core.Infrastructure.Repository;
    using Core.Infrastructure.RepositoryCache;
    using Domain.Agregates.Repositories;
    using Domain.Entities.Address.Repositories;
    using Domain.Entities.Channel.Repositories;
    using global::Autofac;
    using Infrastructure.Persistence;

    /// <summary>
    ///     Infrastructure module.
    /// </summary>
    public sealed class InfrastructureModule : Module
    {
        /// <summary>
        ///     Load services into container.
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConnectionFactory>().As<IConnectionFactory>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            builder.RegisterType<EconomicActivityRepository>().As<IEconomicActivityRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterDecorator<CacheEconomicActivityRepository, IEconomicActivityRepository>();

            builder.RegisterType<CivilityRepository>().As<ICivilityRepository>().InstancePerLifetimeScope();
            builder.RegisterDecorator<CacheCivilityRepository, ICivilityRepository>();

            builder.RegisterType<PartnerRepository>().As<IPartnerRepository>().InstancePerLifetimeScope();
            builder.RegisterDecorator<CachePartnerRepository, IPartnerRepository>();

            builder.RegisterType<CountryRepository>().As<ICountryRepository>().InstancePerLifetimeScope();
            builder.RegisterDecorator<CacheCountryRepository, ICountryRepository>();

            builder.RegisterType<EmailTemplateRepository>().As<IEmailTemplateRepository>().InstancePerLifetimeScope();
            builder.RegisterDecorator<CacheEmailTemplateRepository, IEmailTemplateRepository>();

            builder.RegisterType<ReferentialService>().As<IReferentialService>().InstancePerLifetimeScope();

            builder.RegisterType<ChannelConfirmationRepository>().As<IChannelConfirmationRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<PasswordRepository>().As<IPasswordRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ChannelRepository>().As<IChannelRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AddressRepository>().As<IAddressRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
        }
    }
}