namespace Bizca.Core.Infrastructure.Database
{
    using Configuration;
    using Microsoft.Azure.Services.AppAuthentication;
    using Microsoft.Extensions.Options;
    using System.Data;
    using System.Data.SqlClient;

    public sealed class ConnectionFactory : IConnectionFactory
    {
        private readonly IDatabaseConfiguration _databaseConfiguration;

        public ConnectionFactory(IOptions<DatabaseConfiguration> databaseOptions)
        {
            _databaseConfiguration = databaseOptions.Value;
        }

        public IDbConnection CreateConnection()
        {
            SqlConnection connection = default;
            try
            {
                connection = new SqlConnection(_databaseConfiguration.ConnectionString);
                if (_databaseConfiguration.UseAzureIdentity)
                    connection.AccessToken = new AzureServiceTokenProvider()
                        .GetAccessTokenAsync("https://database.windows.net/").Result;

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