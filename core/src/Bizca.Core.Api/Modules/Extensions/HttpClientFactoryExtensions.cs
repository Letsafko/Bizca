namespace Bizca.Core.Api.Modules.Extensions
{
    using Infrastructure;
    using Infrastructure.Extension;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    public static class HttpClientFactoryExtensions
    {
        public static IHttpClientBuilder AddHttpClientBase<TClient, TImplementation, TOption>(
            this IServiceCollection services,
            IConfigurationSection section,
            string httpClientName = null)
            where TClient : class
            where TImplementation : class, TClient
            where TOption : class, IAgentConfiguration//, new()
        {
            return services
                .Configure<TOption>(section)
                .AddHttpClient<TClient, TImplementation>(
                    !string.IsNullOrWhiteSpace(httpClientName) ? httpClientName : typeof(TOption).GetGenericTypeName(),
                    (provider, client) =>
                    {
                        TOption option = provider.GetRequiredService<IOptions<TOption>>().Value;
                        client.BaseAddress = option.BaseAddress;
                        if (option.Timeout.HasValue)
                            client.Timeout = option.Timeout.Value;
                    });
        }
    }
}