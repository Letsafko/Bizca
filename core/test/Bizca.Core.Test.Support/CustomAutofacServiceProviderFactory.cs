namespace Bizca.Core.Test.Support
{
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;

    public class CustomAutofacServiceProviderFactory : IServiceProviderFactory<ContainerBuilder>
    {
        private readonly AutofacServiceProviderFactory _wrapped = new();
        private IServiceCollection? _services;

        public ContainerBuilder CreateBuilder(IServiceCollection? services)
        {
            _services = services;
            return _wrapped.CreateBuilder(services);
        }

        public IServiceProvider CreateServiceProvider(ContainerBuilder containerBuilder)
        {
            var filters = _services
                .BuildServiceProvider()
#pragma warning disable CS0612
                .GetRequiredService<IEnumerable<IStartupConfigureContainerFilter<ContainerBuilder>>>();
#pragma warning restore CS0612

            foreach (var filter in filters) filter.ConfigureContainer(b => { })(containerBuilder);

            return _wrapped.CreateServiceProvider(containerBuilder);
        }
    }
}