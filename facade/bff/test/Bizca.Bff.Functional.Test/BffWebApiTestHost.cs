﻿namespace Bizca.Bff.Functional.Test
{
    using Bizca.Bff.WebApi;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Net.Http;

    public sealed class BffWebApiTestHost : IDisposable
    {
        private readonly WebApplicationFactory<Startup> webApplicationFactory;
        private bool disposed;
        public BffWebApiTestHost(BffWebApplicationFactory webApplicationFactory)
        {
            this.webApplicationFactory = webApplicationFactory
                .WithWebHostBuilder(builder => ConfigureServices(builder));
        }

        public T GetRequiredService<T>() where T : class
        {
            return webApplicationFactory.Services.GetRequiredService<T>();
        }

        public HttpClient CreateClient()
        {
            return webApplicationFactory.CreateClient();
        }


        private IServiceCollection ConfigureServices(IWebHostBuilder builder)
        {
            return null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (!disposed && disposing)
                webApplicationFactory?.Dispose();

            disposed = true;
        }
    }
}