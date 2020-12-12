namespace Bizca.Core.Infrastructure.Abstracts
{
    using Bizca.Core.Infrastructure.Abstracts.Configuration;
    using System.Data;

    public interface IConnectionFactory
    {
        /// <summary>
        ///     create an instance of <see cref="IDbConnection"/>
        /// </summary>
        IDbConnection CreateConnection<T>() where T : class, IDatabaseConfiguration, new();
    }
}
