namespace Bizca.Core.Infrastructure.Database
{
    using Bizca.Core.Infrastructure.Database.Configuration;
    using System.Data;

    public interface IConnectionFactory
    {
        /// <summary>
        ///     create an instance of <see cref="IDbConnection"/>
        /// </summary>
        IDbConnection CreateConnection<T>() where T : class, IDatabaseConfiguration, new();
    }
}