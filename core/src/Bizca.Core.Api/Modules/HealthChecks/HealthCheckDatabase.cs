namespace Bizca.Core.Api.Modules.HealthChecks
{
    using Dapper;
    using Infrastructure.Database.Configuration;
    using Microsoft.Azure.Services.AppAuthentication;
    using Microsoft.Data.SqlClient;
    using Microsoft.Extensions.Diagnostics.HealthChecks;
    using Microsoft.Extensions.Options;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class HealthCheckDatabase : IHealthCheck
    {
        private readonly IDatabaseConfiguration _databaseConfiguration;

        public HealthCheckDatabase(IOptions<DatabaseConfiguration> databaseOptions)
        {
            _databaseConfiguration = databaseOptions.Value;
        }
    
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                await using var connection = new SqlConnection(_databaseConfiguration.ConnectionString);
                if (_databaseConfiguration.UseAzureIdentity)
                    connection.AccessToken = await new AzureServiceTokenProvider()
                        .GetAccessTokenAsync("https://database.windows.net/", cancellationToken: cancellationToken);

                await connection.OpenAsync(cancellationToken);
                await connection.ExecuteAsync("select 1");
                return HealthCheckResult.Healthy("ok");
            }
            catch
            {
                return HealthCheckResult.Unhealthy("connection to database failed");
            }
        }
    }
}