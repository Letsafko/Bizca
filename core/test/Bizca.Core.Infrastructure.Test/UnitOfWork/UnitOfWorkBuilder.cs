namespace Bizca.Core.Infrastructure.Test
{
    using Bizca.Core.Infrastructure.Database;
    using Bizca.Core.Infrastructure.Database.Configuration;
    using NSubstitute;
    using System.Data;

    public sealed class UnitOfWorkBuilder
    {
        private IConnectionFactory _connectionFactory;
        private IDbTransaction _transaction;
        private IDbConnection _connection;
        private UnitOfWorkBuilder()
        {
            _connectionFactory = Substitute.For<IConnectionFactory>();
            _transaction = Substitute.For<IDbTransaction>();
            _connection = Substitute.For<IDbConnection>();
        }

        public static UnitOfWorkBuilder Instance => new UnitOfWorkBuilder();
        public UnitOfWork Build()
        {
            return new UnitOfWork(_connectionFactory);
        }

        public UnitOfWorkBuilder WithConnectionFactory<T>() where T : class, IDatabaseConfiguration, new()
        {
            _connectionFactory
                .CreateConnection<T>()
                .Returns(_connection);

            return this;
        }

        public UnitOfWorkBuilder WithDbTransaction()
        {
            _connection
                .BeginTransaction(IsolationLevel.ReadCommitted)
                .Returns(_transaction);

            return this;
        }
    }
}