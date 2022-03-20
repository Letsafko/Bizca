namespace Bizca.Core.Infrastructure
{
    using Bizca.Core.Domain;
    using Dapper.FastCrud;
    using Dapper.FastCrud.Configuration.StatementOptions.Builders;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Reflection;
    using System.Threading.Tasks;

    public class BaseRepository<T>
    {
        protected readonly IUnitOfWork UnitOfWork;
        public BaseRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }


        protected static string GetColumnAttributeName(string propertyName)
        {
            return typeof(T)
                .GetProperty(propertyName)
                .GetCustomAttribute<ColumnAttribute>()
                ?.Name;
        }

        protected Task<T> GetAsync(T entity)
        {
            return UnitOfWork.Connection.GetAsync(entity, AttachTransaction);
        }


        protected Task<IEnumerable<T>> FindAsync(Action<IRangedBatchSelectSqlSqlStatementOptionsOptionsBuilder<T>> statement)
        {
            return UnitOfWork.Connection.FindAsync(statement);
        }

        protected Task InsertAsync(T entity)
        {
            return UnitOfWork.Connection.InsertAsync(entity, AttachTransaction);
        }

        protected Task<bool> UpdateAsync(T entity)
        {
            return UnitOfWork.Connection.UpdateAsync(entity, AttachTransaction);
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
