namespace Bizca.User.WebApi.Modules
{
    using Autofac;
    using Bizca.User.Application.UseCases.GetUserDetail;
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
            builder.RegisterAssemblyTypes(typeof(GetUserDetailQuery).Assembly).AsClosedTypesOf(typeof(IRequestHandler<,>));
        }
    }
}
