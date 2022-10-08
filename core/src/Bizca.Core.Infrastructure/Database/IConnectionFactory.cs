namespace Bizca.Core.Infrastructure.Database
{
    using System.Data;

    public interface IConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}