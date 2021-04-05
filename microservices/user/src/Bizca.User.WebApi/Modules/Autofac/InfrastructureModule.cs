namespace Bizca.User.WebApi.Modules.Autofac
{
    using Bizca.Core.Domain;
    using Bizca.Core.Domain.Civility;
    using Bizca.Core.Domain.Country;
    using Bizca.Core.Domain.EconomicActivity;
    using Bizca.Core.Domain.Partner;
    using Bizca.Core.Infrastructure;
    using Bizca.Core.Infrastructure.Cache;
    using Bizca.Core.Infrastructure.Database;
    using Bizca.Core.Infrastructure.Persistance;
    using Bizca.User.Domain.Agregates.Repositories;
    using Bizca.User.Domain.Entities.Address.Repositories;
    using Bizca.User.Domain.Entities.Channel.Repositories;
    using Bizca.User.Infrastructure.Persistance;
    using global::Autofac;

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

            builder.RegisterType<EconomicActivityRepository>().As<IEconomicActivityRepository>().InstancePerLifetimeScope();
            builder.RegisterDecorator<CacheEconomicActivityRepository, IEconomicActivityRepository>();

            builder.RegisterType<CivilityRepository>().As<ICivilityRepository>().InstancePerLifetimeScope();
            builder.RegisterDecorator<CacheCivilityRepository, ICivilityRepository>();

            builder.RegisterType<PartnerRepository>().As<IPartnerRepository>().InstancePerLifetimeScope();
            builder.RegisterDecorator<CachePartnerRepository, IPartnerRepository>();

            builder.RegisterType<CountryRepository>().As<ICountryRepository>().InstancePerLifetimeScope();
            builder.RegisterDecorator<CacheCountryRepository, ICountryRepository>();

            builder.RegisterType<ChannelConfirmationRepository>().As<IChannelConfirmationRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PasswordRepository>().As<IPasswordRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ChannelRepository>().As<IChannelRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AddressRepository>().As<IAddressRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
        }
    }
}