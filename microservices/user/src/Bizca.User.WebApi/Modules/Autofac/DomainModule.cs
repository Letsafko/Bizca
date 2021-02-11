namespace Bizca.User.WebApi.Modules.Autofac
{
    using Bizca.Core.Domain.Rules;
    using Bizca.Core.Domain.Services;
    using Bizca.User.Domain.Agregates.BusinessCheck;
    using Bizca.User.Domain.Agregates.BusinessCheck.Rules;
    using Bizca.User.Domain.Agregates.Factories;
    using global::Autofac;

    /// <summary>
    ///     Domain module.
    /// </summary>
    public sealed class DomainModule : Module
    {
        /// <summary>
        ///     Load services into container.
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserFactory>().As<IUserFactory>();
            builder.RegisterType<ReferentialService>().As<IReferentialService>();
            builder.RegisterAssemblyTypes(typeof(UserRuleEngine).Assembly).AsClosedTypesOf(typeof(IBusinessRuleEngine<>));
            builder.RegisterAssemblyTypes(typeof(UserMustBeUniqueByPartner).Assembly).AsClosedTypesOf(typeof(IBusinessRule<>));
        }
    }
}