namespace Bizca.User.WebApi.Modules
{
    using Autofac;
    using Bizca.Core.Domain;
    using Bizca.Core.Domain.Civility;
    using Bizca.Core.Domain.Country;
    using Bizca.Core.Domain.EconomicActivity;
    using Bizca.Core.Domain.Partner;
    using Bizca.Core.Infrastructure;
    using Bizca.Core.Infrastructure.Abstracts;
    using Bizca.User.Domain.Agregates.Users.Repositories;
    using Bizca.User.Infrastructure;

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
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<ConnectionFactory>().As<IConnectionFactory>().InstancePerLifetimeScope();

            builder.RegisterType<CountryRepository>().As<ICountryRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PartnerRepository>().As<IPartnerRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CivilityRepository>().As<ICivilityRepository>().InstancePerLifetimeScope();
            builder.RegisterType<EconomicActivityRepository>().As<IEconomicActivityRepository>().InstancePerLifetimeScope();

            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UserChannelRepository>().As<IUserChannelRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UserChannelConfirmationRepository>().As<IUserChannelConfirmationRepository>().InstancePerLifetimeScope();
        }
    }
}
