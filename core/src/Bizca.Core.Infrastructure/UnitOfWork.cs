namespace Bizca.Core.Infrastructure
{
    using Bizca.Core.Domain;
    using Bizca.Core.Infrastructure.Database;
    using Bizca.Core.Infrastructure.Database.Configuration;
    using System;
    using System.Data;

    public sealed class UnitOfWork : IUnitOfWork
    {
        #region fields & ctor

        public UnitOfWork(IConnectionFactory connectionFactory)
        {
            if (connectionFactory is null)
                throw new ArgumentNullException(nameof(connectionFactory));

            Connection = connectionFactory.CreateConnection<DatabaseConfiguration>();
        }

        private bool disposed;

        #endregion

        public IDbTransaction Transaction { get; private set; }
        public IDbConnection Connection { get; private set; }

        public void Rollback()
        {
            Transaction.Rollback();
        }

        public void Commit()
        {
            Transaction.Commit();
        }

        public void Begin()
        {
            Transaction = Connection.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #region private helpers

        private void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                Transaction?.Dispose();
                Connection?.Dispose();
                Transaction = null;
                Connection = null;
            }
            disposed = true;
        }

        #endregion
    }
}