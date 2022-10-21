namespace Bizca.Core.Api.Modules.HealthChecks
{
    using Dapper;
    using Infrastructure.Database;
    using Microsoft.Extensions.Diagnostics.HealthChecks;
    using System.Data;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class HealthCheckDatabase : IHealthCheck
    {
        private readonly IDbConnection _dbConnection;
        public HealthCheckDatabase(IUnitOfWork unitOfWork)
            => _dbConnection = unitOfWork.Connection;
        
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                await _dbConnection.ExecuteAsync("select 1");
                return HealthCheckResult.Healthy("ok");
            }
            catch
            {
                return HealthCheckResult.Unhealthy("connection to database failed");
            }
            finally
            {
                _dbConnection.Dispose();
            }
        }
    }
}