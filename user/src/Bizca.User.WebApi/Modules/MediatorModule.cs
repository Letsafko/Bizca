namespace Bizca.User.WebApi.Modules
{
    using Autofac;
    using Bizca.Core.Application;
    using Bizca.Core.Application.Abstracts;
    using MediatR;

    /// <summary>
    ///     Mediator module.
    /// </summary>
    public sealed class MediatorModule : Module
    {
        /// <summary>
        ///     Load services into container.
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Processor>().As<IProcessor>();
            builder.RegisterAssemblyTypes(typeof(IMediator).Assembly).AsImplementedInterfaces();
            builder.Register<ServiceFactory>(context =>
            {
                IComponentContext componentContext = context.Resolve<IComponentContext>();
                return t => componentContext.TryResolve(t, out object o) ? o : default;
            });
        }
    }
}