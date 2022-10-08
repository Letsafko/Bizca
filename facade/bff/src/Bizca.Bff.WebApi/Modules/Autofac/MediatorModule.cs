namespace Bizca.Bff.WebApi.Modules.Autofac
{
    using Core.Application;
    using global::Autofac;
    using MediatR;

    /// <summary>
    ///     Mediator modules.
    /// </summary>
    public sealed class MediatorModule : Module
    {
        /// <summary>
        ///     Load services.
        /// </summary>
        /// <param name="builder">container builder.</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Processor>().As<IProcessor>();
            builder.RegisterAssemblyTypes(typeof(IMediator).Assembly).AsImplementedInterfaces();
            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => componentContext.TryResolve(t, out object o) ? o : default;
            });
        }
    }
}