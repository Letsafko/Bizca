namespace Bizca.Core.Infrastructure
{
    using Dapper.FastCrud;
    using Dapper.FastCrud.Configuration.StatementOptions.Builders;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public abstract class BaseRepository<T>
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected BaseRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected Task<T> GetAsync(T entity)
        {
            return UnitOfWork.Connection.GetAsync(entity, AttachTransaction);
        }

        protected Task<IEnumerable<T>> FindAsync(
            Action<IRangedBatchSelectSqlSqlStatementOptionsOptionsBuilder<T>> statement)
        {
            return UnitOfWork.Connection.FindAsync(statement);
        }

        protected Task InsertAsync(T entity)
        {
            return UnitOfWork.Connection.InsertAsync(entity, AttachTransaction);
        }
        
        private void AttachTransaction(IStandardSqlStatementOptionsBuilder<T> statement)
        {
            if (UnitOfWork.Transaction != null)
            {
                statement.AttachToTransaction(UnitOfWork.Transaction);
            }
        }

        private void AttachTransaction(ISelectSqlStatementOptionsBuilder<T> statement)
        {
            if (UnitOfWork.Transaction != null)
            {
                statement.AttachToTransaction(UnitOfWork.Transaction);
            }
        }
    }
}
