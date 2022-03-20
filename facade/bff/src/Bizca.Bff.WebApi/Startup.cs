namespace Bizca.Bff.WebApi
{
    using Autofac;
    using Bizca.Bff.Application.UseCases.CreateNewUser;
    using Bizca.Bff.Domain.Wrappers.Contact;
    using Bizca.Bff.Domain.Wrappers.Notification;
    using Bizca.Bff.Domain.Wrappers.Users;
    using Bizca.Bff.Infrastructure.Wrappers;
    using Bizca.Bff.Infrastructure.Wrappers.Contact;
    using Bizca.Bff.Infrastructure.Wrappers.Notifications;
    using Bizca.Bff.Infrastructure.Wrappers.Notifications.Configurations;
    using Bizca.Bff.Infrastructure.Wrappers.Users;
    using Bizca.Bff.WebApi.Modules.Autofac;
    using Bizca.Bff.WebApi.Modules.Extensions;
    using Bizca.Core.Api;
    using Bizca.Core.Api.Modules.Extensions;
    using Bizca.Core.Api.Modules.HealthChecks;
    using Bizca.Core.Infrastructure.Database.Configuration;
    using FluentValidation.AspNetCore;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    ///     Startup
    /// </summary>
    public sealed class Startup : StartupExtended
    {
        /// <summary>
        ///     Creates an instance of <see cref="Startup"/>
        /// </summary>
        /// <param name="environment"></param>
        /// <param name="configuration"></param>
        public Startup(IHostEnvironment environment, IConfiguration configuration) : base(configuration, environment)
        {
        }

        private static readonly string SendInBlueSheme = $"Api:Dependencies:{nameof(NotificationSettings)}";
        private static readonly string ApiUserScheme = $"Api:Dependencies:{nameof(UserSettings)}";
        private readonly string SendInBlueApiKey = $"{SendInBlueSheme}:ApiKey";
        private const string DatabaseScheme = "BizcaDatabase";

        /// <summary>
        ///     Configures services.
        /// </summary>
        /// <param name="services">service collection.</param>
        new public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient(_ => new AuthorisationDelegateHandler(configuration.GetValue<string>(SendInBlueApiKey)));

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
        new public void Configure(IApplicationBuilder app)
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