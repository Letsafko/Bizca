﻿namespace Bizca.Core.Infrastructure
{
    using Database;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Data;

    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly ILogger<UnitOfWork> _logger;
        public UnitOfWork(IConnectionFactory connectionFactory, ILogger<UnitOfWork> logger)
        {
            Connection = connectionFactory.CreateConnection();
            _logger = logger;
        }

        ~UnitOfWork() => Dispose(false);
        
        private bool _disposed;

        public IDbTransaction Transaction { get; private set; }
        public IDbConnection Connection { get; }

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

        public void Dispose()
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
            }
            _disposed = true;
        }
    }
}