namespace Bizca.Notification.WebApi.Modules.Autofac
{
    using Core.Infrastructure.Database;
    using global::Autofac;

    internal class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConnectionFactory>().As<IConnectionFactory>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
        }
    }
}