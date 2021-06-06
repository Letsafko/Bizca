namespace Bizca.Core.Api.Modules.Extensions
{
    using Bizca.Core.Infrastructure;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    public static class HttpClientFactoryExtensions
    {
        /// <summary>
        ///     Use to inject http client to specific class
        ///     Note that all classes in call stack shoulb be injected as transient
        ///     For now, it only be set the baseAddress
        /// </summary>
        /// <typeparam name="TClient">Interface</typeparam>
        /// <typeparam name="TImplementation">Implementation</typeparam>
        /// <typeparam name="TOption">Agent configuration</typeparam>
        /// <param name="services"> service collection</param>
        /// <param name="httpMessageHandler">custom http message handler.</param>
        public static IServiceCollection AddHttpClientBase<TClient, TImplementation, TOption>(this IServiceCollection services, 
            IConfigurationSection section, 
            string httpClientName = null)
            where TClient : class
            where TImplementation : class, TClient
            where TOption : class, IAgentConfiguration, new()
        {
            return services.Configure<TOption>(section)
                    .AddHttpClient<TClient, TImplementation>(!string.IsNullOrWhiteSpace(httpClientName) ? httpClientName : typeof(TOption).Name,
                        (provider, client) =>
                        {
                            TOption option = provider.GetRequiredService<IOptions<TOption>>().Value;
                            client.BaseAddress = option.BaseAddress;
                            if (option.Timeout.HasValue)
                                client.Timeout = option.Timeout.Value;
                        })
                    .Services;
        }
    }
}
