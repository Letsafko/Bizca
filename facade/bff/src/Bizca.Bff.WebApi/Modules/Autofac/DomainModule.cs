namespace Bizca.Bff.WebApi.Modules.Autofac
{
    using Domain.Entities.Subscription.Factories;
    using Domain.Entities.User.Factories;
    using global::Autofac;

    /// <summary>
    ///     Domain modules.
    /// </summary>
    public sealed class DomainModule : Module
    {
        /// <summary>
        ///     Load services.
        /// </summary>
        /// <param name="builder">container builder.</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SubscriptionFactory>().As<ISubscriptionFactory>();
            builder.RegisterType<UserFactory>().As<IUserFactory>();
        }
    }
}