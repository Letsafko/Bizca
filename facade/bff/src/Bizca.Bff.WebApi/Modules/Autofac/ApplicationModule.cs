namespace Bizca.Bff.WebApi.Modules.Autofac
{
    using Application.UseCases.CreateNewUser;
    using Application.UseCases.SendEmail;
    using Core.Domain.Cqrs.Services;
    using Core.Infrastructure.Behaviors;
    using global::Autofac;
    using MediatR;

    /// <summary>
    ///     Application modules.
    /// </summary>
    public sealed class ApplicationModule : Module
    {
        /// <summary>
        ///     Load services.
        /// </summary>
        /// <param name="builder">container builder.</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(SendTransactionalEmailUseCase).Assembly)
                .AsClosedTypesOf(typeof(INotificationHandler<>));
            builder.RegisterAssemblyTypes(typeof(CreateUserUseCase).Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));
            builder.RegisterGeneric(typeof(UnitOfWorkCommandBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(LoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(ValidationBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterType<EventService>().As<IEventService>().InstancePerLifetimeScope();
        }
    }
}