namespace Bizca.Core.Infrastructure
{
    using Bizca.Core.Infrastructure.Abstracts;
    using Bizca.Core.Infrastructure.Abstracts.Configuration;
    using System;
    using System.Data;

    public sealed class UnitOfWork : IUnitOfWork
    {
        #region fields & ctor

        public UnitOfWork(IConnectionFactory connectionFactory)
        {
            if (connectionFactory is null)
                throw new ArgumentNullException(nameof(connectionFactory));

            Connection = connectionFactory.CreateConnection<BizcaDatabaseConfiguration>();
        }

        private bool _disposed;

        #endregion

        public IDbConnection Connection { get; private set; }
        public IDbTransaction Transaction { get; private set; }

        #region methods

        public void Rollback()
        {
            try
            {
                Transaction.Rollback();
            }
            finally
            {
                Reset();
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
                Reset();
            }
        }

        public void Begin()
        {
            Transaction = Connection.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        #endregion

        #region private helpers

        public void Reset()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                Transaction?.Dispose();
                Connection?.Dispose();
                Transaction = null;
                Connection = null;
            }
            _disposed = true;
        }

        #endregion
    }
}
