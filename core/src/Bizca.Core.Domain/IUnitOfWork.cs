namespace Bizca.Core.Domain
{
    using System;
    using System.Data;
    public interface IUnitOfWork : IDisposable
    {
        void Begin();
        void Commit();
        void Rollback();
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
    }
}
