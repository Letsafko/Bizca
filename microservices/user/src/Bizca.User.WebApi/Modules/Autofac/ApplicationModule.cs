﻿namespace Bizca.User.WebApi.Modules.Autofac
{
    using Bizca.Core.Application.Behaviors;
    using Bizca.Core.Application.Services;
    using Bizca.User.Application.UseCases.GetUserDetail;
    using global::Autofac;
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
            builder.RegisterGeneric(typeof(UnitOfWorkCommandBehavior<>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(LoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(ValidationBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterType<EventService>().As<IEventService>().InstancePerLifetimeScope();
        }
    }
}