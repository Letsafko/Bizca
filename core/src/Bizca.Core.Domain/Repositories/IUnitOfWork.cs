using System.Data;

namespace Bizca.Core.Domain.Repositories
{
    public interface IUnitOfWork
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }

        void Begin();
        void Commit();
        void Rollback();
        IRepository GetRepository<TRepository>() where TRepository : IRepository;
    }
}
