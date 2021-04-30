namespace Bizca.Bff.WebApi.Modules.Autofac
{
    using Bizca.Core.Domain;
    using Bizca.Core.Infrastructure;
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
        }
    }
}