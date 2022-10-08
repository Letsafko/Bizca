namespace Bizca.Core.Infrastructure
{
    using System;
    using System.Data;
    public interface IUnitOfWork : IDisposable
    {
        IDbTransaction Transaction { get; }
        IDbConnection Connection { get; }
        void Rollback();
        void Commit();
        void Begin();
    }
}