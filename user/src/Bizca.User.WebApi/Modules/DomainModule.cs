namespace Bizca.User.WebApi.Modules
{
    using Autofac;
    using Bizca.Core.Domain.Rules;
    using Bizca.User.Domain.Agregates.Users.Rules;

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
            builder.RegisterAssemblyTypes(typeof(UserRuleEngine).Assembly).AsClosedTypesOf(typeof(IBusinessRuleEngine<>));
            builder.RegisterAssemblyTypes(typeof(UserMustBeUniqueByPartner).Assembly).AsClosedTypesOf(typeof(IBusinessRule<>));
        }
    }
}
