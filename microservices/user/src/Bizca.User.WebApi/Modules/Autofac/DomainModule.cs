namespace Bizca.User.WebApi.Modules.Autofac
{
    using Core.Domain.Referential.Services;
    using Core.Domain.Rules;
    using Domain.Agregates;
    using Domain.Agregates.Factories;
    using Domain.BusinessCheck.UserRule;
    using Domain.Entities.Address.Factories;
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
            builder.RegisterType<AddressFactory>().As<IAddressFactory>();
            builder.RegisterType<PasswordHasher>().As<IPasswordHasher>();
            builder.RegisterType<ReferentialService>().As<IReferentialService>();
            builder.RegisterAssemblyTypes(typeof(UserRuleEngine).Assembly)
                .AsClosedTypesOf(typeof(IBusinessRuleEngine<>));
            builder.RegisterAssemblyTypes(typeof(UserMustBeUniqueByPartner).Assembly)
                .AsClosedTypesOf(typeof(IBusinessRule<>));
        }
    }
}