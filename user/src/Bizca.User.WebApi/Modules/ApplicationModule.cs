namespace Bizca.User.WebApi.Modules
{
    using Autofac;
    using Bizca.Core.Application.Behaviors;
    using Bizca.User.Application.UseCases.GetUser.Detail;
    using MediatR;

    /// <summary>
    ///     Application module.
    /// </summary>
    public sealed class ApplicationModule : Module
    {
        /// <summary>
        ///     Load services into container.
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(UnitOfWorkCommandBehavior<>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterAssemblyTypes(typeof(GetUserDetailQuery).Assembly).AsClosedTypesOf(typeof(IRequestHandler<,>));
        }
    }
}
