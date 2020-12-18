namespace Bizca.Core.Infrastructure.Abstracts
{
    using System.Data;
    public interface IUnitOfWork
    {
        void Begin();
        void Commit();
        void Rollback();
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
    }
}
