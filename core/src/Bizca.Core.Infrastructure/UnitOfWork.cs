namespace Bizca.Core.Infrastructure
{
    using Bizca.Core.Domain.Repositories;
    using Bizca.Core.Infrastructure.Abstracts;
    using Bizca.Core.Infrastructure.Abstracts.Configuration;
    using System;
    using System.Collections.Generic;
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
        private readonly IDictionary<Type, IRepository> _repositories = new Dictionary<Type, IRepository>();

        #endregion

        public IDbConnection Connection { get; private set; }
        public IDbTransaction Transaction { get; private set; }

        #region methods

        public IRepository GetRepository<TRepository>() where TRepository : IRepository
        {
            if (!_repositories.ContainsKey(typeof(TRepository)))
                _repositories[typeof(TRepository)] = Activator.CreateInstance(typeof(TRepository), this) as IRepository;

            return _repositories[typeof(TRepository)];
        }

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
