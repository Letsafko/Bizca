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

        public IDbConnection Connection { get; private set; }
        public IDbTransaction Transaction { get; private set; }

        public void Rollback()
        {
            try
            {
                Transaction.Rollback();
            }
            finally
            {
                Dispose();
            }
        }

        public void Commit()
        {
            try
            {
                Transaction.Commit();
            }
            finally
            {
                Dispose();
            }
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