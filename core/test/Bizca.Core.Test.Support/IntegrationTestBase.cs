namespace Bizca.Core.Test.Support
{
    using Autofac;
    using AutoFixture;
    using Infrastructure.Database;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Xunit;

    public abstract class IntegrationTestBase<TStartup> : IClassFixture<CustomAutofacWebApplicationFactory<TStartup>>,
        IAsyncLifetime
        where TStartup : class
    {
        private WebApplicationFactory<TStartup> _webApplicationFactory;
        protected IUnitOfWork UnitOfWork { get; }

        protected IntegrationTestBase(CustomAutofacWebApplicationFactory<TStartup> webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
            UnitOfWork = GetService<IUnitOfWork>();
        }

        protected HttpClient HttpClient => _webApplicationFactory.CreateClient();
        
        protected TService GetService<TService>() where TService : notnull
        {
            return _webApplicationFactory.Services.GetRequiredService<TService>();
        }

        protected void OverrideServices(Action<ContainerBuilder>? servicesConfiguration)
        {
            _webApplicationFactory = _webApplicationFactory
                .WithWebHostBuilder(builder =>
                {
                    if (servicesConfiguration != null) builder.ConfigureTestContainer(servicesConfiguration);
                });
        }

        public virtual Task InitializeAsync()
        {
            UnitOfWork.Begin();
            return Task.CompletedTask;
        }

        public Task DisposeAsync()
        {
            UnitOfWork.Rollback();
            return Task.CompletedTask;
        }
    }
}