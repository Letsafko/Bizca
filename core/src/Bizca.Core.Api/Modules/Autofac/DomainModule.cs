namespace Bizca.Core.Api.Modules.Autofac
{
    using Domain.Cqrs;
    using global::Autofac;
    using MediatR;

    public class DomainModule: Module
    {
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