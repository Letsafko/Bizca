namespace Bizca.Core.Infrastructure.Database
{
    using Microsoft.Extensions.Logging;
    using System;
    using System.Data;

    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly ILogger<UnitOfWork> _logger;
    
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UnitOfWork() => Dispose(false);

        public UnitOfWork(IConnectionFactory connectionFactory, ILogger<UnitOfWork> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }

        public IDbTransaction Transaction { get; private set; }

        private IDbConnection _dbConnection;
        public IDbConnection Connection
        {
            get
            {
                return _dbConnection ??= _connectionFactory.CreateConnection();
            }
        }

        public void Rollback()
        {
            _logger.LogDebug("transaction rollback");
            Transaction.Rollback();
        }

        public void Commit()
        {
            _logger.LogDebug("transaction committed");
            Transaction.Commit();
        }

        public void Begin()
        {
            Transaction = Connection.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        private bool _disposed;
        private void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                Transaction?.Dispose();
                Connection?.Dispose();
            }

            _disposed = true;
        }
    }
}