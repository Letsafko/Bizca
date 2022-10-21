namespace Bizca.Notification.WebApi.Modules.Autofac
{
    using Core.Infrastructure;
    using Core.Infrastructure.Database;
    using Core.Infrastructure.Repository;
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
        }
    }
}