namespace Bizca.Bff.WebApi
{
    using Application.UseCases.CreateNewUser;
    using Autofac;
    using Core.Api;
    using Core.Api.Modules.Extensions;
    using Core.Api.Modules.HealthChecks;
    using Core.Infrastructure.Database.Configuration;
    using Domain.Wrappers.Contact;
    using Domain.Wrappers.Notification;
    using Domain.Wrappers.Users;
    using FluentValidation.AspNetCore;
    using Infrastructure.Wrappers;
    using Infrastructure.Wrappers.Contact;
    using Infrastructure.Wrappers.Notifications;
    using Infrastructure.Wrappers.Notifications.Configurations;
    using Infrastructure.Wrappers.Users;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Modules.Autofac;
    using Modules.Extensions;

    /// <summary>
    ///     Startup
    /// </summary>
    public sealed class Startup : StartupExtended
    {
        private const string DatabaseScheme = "BizcaDatabase";

        private static readonly string SendInBlueSheme = $"Api:Dependencies:{nameof(NotificationSettings)}";
        private static readonly string ApiUserScheme = $"Api:Dependencies:{nameof(UserSettings)}";
        private readonly string SendInBlueApiKey = $"{SendInBlueSheme}:ApiKey";

        /// <summary>
        ///     Creates an instance of <see cref="Startup" />
        /// </summary>
        /// <param name="environment"></param>
        /// <param name="configuration"></param>
        public Startup(IHostEnvironment environment, IConfiguration configuration) : base(configuration, environment)
        {
        }

        /// <summary>
        ///     Configures services.
        /// </summary>
        /// <param name="services">service collection.</param>
        public new void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient(_ =>
                new AuthorisationDelegateHandler(configuration.GetValue<string>(SendInBlueApiKey)));

            services
                .AddHttpClientBase<INotificationWrapper,
                    NotificationWrapper,
                    NotificationSettings>(configuration.GetSection(SendInBlueSheme),
                    NamedHttpClients.ApiNotificationClientName)
                .AddHttpMessageHandler(provider => provider.GetRequiredService<AuthorisationDelegateHandler>());

            services
                .AddHttpClientBase<IContactWrapper,
                    ContactWrapper,
                    ContactSettings>(configuration.GetSection(SendInBlueSheme),
                    NamedHttpClients.ApiProviderName)
                .AddHttpMessageHandler(provider => provider.GetRequiredService<AuthorisationDelegateHandler>());

            services.AddHttpClientBase<IUserWrapper,
                UserWrapper,
                UserSettings>(configuration.GetSection(ApiUserScheme),
                NamedHttpClients.ApiUserClientName);

            services.Configure<DatabaseConfiguration>(configuration.GetSection(DatabaseScheme));
            base.ConfigureServices(services);
            services.ConfigureHealthChecks()
                .AddControllers()
                .AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<CreateUserCommandValidator>());
        }

        /// <summary>
        ///     Configures application builder.
        /// </summary>
        /// <param name="app">application builder.</param>
        public new void Configure(IApplicationBuilder app)
        {
            base.Configure(app);
            app.UseHealthChecks();
        }

        /// <summary>
        ///     Configures autofac container.
        /// </summary>
        /// <param name="builder"></param>
        public static void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new InfrastructureModule());
            builder.RegisterModule(new ApplicationModule());
            builder.RegisterModule(new MediatorModule());
            builder.RegisterModule(new WebApiModule());
            builder.RegisterModule(new DomainModule());
        }
    }
}