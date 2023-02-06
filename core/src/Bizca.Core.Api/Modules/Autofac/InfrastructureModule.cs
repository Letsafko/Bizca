namespace Bizca.Core.Api.Modules.Autofac
{
    using Domain.Aggregate;
    using Domain.Referential.Repository;
    using Domain.Referential.Services;
    using global::Autofac;
    using Infrastructure.Behaviors;
    using Infrastructure.Database;
    using Infrastructure.DomainEventDispatch;
    using Infrastructure.Repository;
    using Infrastructure.RepositoryCache;
    using MediatR;

    public class InfrastructureModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(LoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(ValidationBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(UnitOfWorkCommandBehavior<,>)).As(typeof(IPipelineBehavior<,>));
        
            builder.RegisterType<CapDomainEventDispatcher>().As<IDomainEventsDispatcher>().InstancePerLifetimeScope();
            builder.RegisterType<DomainEventConsumer>().As<IDomainEventConsumer>().InstancePerLifetimeScope();
            builder.RegisterType<TopicNameFormatter>().As<ITopicNameFormatter>().SingleInstance();
        
            builder.RegisterType<ConnectionFactory>().As<IConnectionFactory>().SingleInstance();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            RegisterRepositories(builder);
        }

        private static void RegisterRepositories(ContainerBuilder builder)
        {
            builder.RegisterDecorator<CacheEconomicActivityRepository, IEconomicActivityRepository>();
            builder
                .RegisterType<EconomicActivityRepository>()
                .As<IEconomicActivityRepository>()
                .InstancePerLifetimeScope();
        
            builder.RegisterDecorator<CacheCivilityRepository, ICivilityRepository>();
            builder
                .RegisterType<CivilityRepository>()
                .As<ICivilityRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterDecorator<CachePartnerRepository, IPartnerRepository>();
            builder
                .RegisterType<PartnerRepository>()
                .As<IPartnerRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterDecorator<CacheCountryRepository, ICountryRepository>();
            builder
                .RegisterType<CountryRepository>()
                .As<ICountryRepository>()
                .InstancePerLifetimeScope();
        
            builder.RegisterDecorator<CacheEmailTemplateRepository, IEmailTemplateRepository>();
            builder
                .RegisterType<EmailTemplateRepository>()
                .As<IEmailTemplateRepository>()
                .InstancePerLifetimeScope();
        
            builder
                .RegisterType<ReferentialService>()
                .As<IReferentialService>()
                .InstancePerLifetimeScope();
        }
    }
}