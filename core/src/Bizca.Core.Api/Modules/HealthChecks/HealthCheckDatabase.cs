namespace Bizca.Core.Api.Modules.HealthChecks
{
    using Bizca.Core.Api.Modules.Extensions;
    using Microsoft.Azure.Services.AppAuthentication;
    using Microsoft.Extensions.Diagnostics.HealthChecks;
    using Microsoft.Extensions.Hosting;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    ///     create an instance of <see cref="HealthCheckDatabase<typeparamref name="T"/>"/>
    /// </summary>
    public abstract class HealthCheckDatabase : IHealthCheck
    {
        #region fields and consts

        private readonly bool useAzureIdentity;
        private readonly string connectionString;
        private readonly IHostEnvironment environment;

        #endregion

        #region constructor

        /// <summary>
        /// create an instance of <see cref="HealthCheckDatabase"/>
        /// </summary>
        /// <param name="environment">host environnement</param>
        /// <param name="connectionString">connection string</param>
        protected HealthCheckDatabase(IHostEnvironment environment, string connectionString, bool useAzureIdentity = false)
        {
            this.environment = environment;
            this.useAzureIdentity = useAzureIdentity;
            this.connectionString = connectionString;
        }

        #endregion

        #region methods

        /// <summary>
        ///     check resource
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            string message = "database";

            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    if (environment.IsDevEnvironment())
                    {
                        message = connectionString;
                    }

                    if (useAzureIdentity)
                    {
                        connection.AccessToken = new AzureServiceTokenProvider().GetAccessTokenAsync("https://database.windows.net/").Result;
                    }

                    connection.Open();
                    cmd.CommandText = "select 1";
                    cmd.CommandType = CommandType.Text;
                    await cmd.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
                    return HealthCheckResult.Healthy($"(OK) {message}");
                }
            }
            catch (Exception ex) when (ex is SqlException)
            {
                return HealthCheckResult.Degraded($"(FAILED) {message}, sql exception error");
            }
            catch (Exception ex) when (ex is TaskCanceledException)
            {
                return HealthCheckResult.Degraded($"(FAILED) {message}, task cancelled");
            }
            catch (Exception ex) when (ex is TimeoutException)
            {
                return HealthCheckResult.Degraded($"(FAILED) {message}, did not respond in time");
            }
            catch (Exception)
            {
                return HealthCheckResult.Degraded($"(FAILED) {message}, internal error");
            }
        }

        #endregion

    }
}