namespace Bizca.Core.Infrastructure.Persistance
{
    using Bizca.Core.Infrastructure.Database;
    using Bizca.Core.Infrastructure.Database.Configuration;
    using Microsoft.Azure.Services.AppAuthentication;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class ConnectionFactory : IConnectionFactory
    {
        #region fields & ctor

        /// <summary>
        ///     create an instance of <see cref="ConnectionFactory"/>
        /// </summary>
        /// <param name="provider">service provider</param>
        public ConnectionFactory(IServiceProvider provider)
        {
            this.provider = provider;
        }
        private readonly IServiceProvider provider;

        #endregion

        /// <summary>
        ///     create an instance of <see cref="IDbConnection"/> according to specific <see cref="T"/> configuration
        /// </summary>
        /// <typeparam name="T">database configuration</typeparam>
        /// <returns><see cref="IDbConnection"/></returns>
        public IDbConnection CreateConnection<T>() where T : class, IDatabaseConfiguration, new()
        {
            SqlConnection connection = default;
            try
            {
                IOptions<T> configuration = provider.GetRequiredService<IOptions<T>>();
                if (string.IsNullOrWhiteSpace(configuration?.Value?.ConnectionString))
                {
                    throw new InvalidOperationException($"missing configuration for {typeof(T).Name}");
                }

                connection = new SqlConnection(configuration.Value.ConnectionString);
                if (configuration.Value.UseAzureIdentity)
                {
                    connection.AccessToken = new AzureServiceTokenProvider().GetAccessTokenAsync("https://database.windows.net/").Result;
                }

                connection.Open();
                return connection;
            }
            catch
            {
                connection?.Close();
                connection?.Dispose();
                throw;
            }
        }
    }
}