namespace Bizca.Core.Infrastructure.Test
{
    using Database;
    using Database.Configuration;
    using NSubstitute;
    using System.Data;

    public sealed class UnitOfWorkBuilder
    {
        private readonly IDbConnection _connection;
        private readonly IConnectionFactory _connectionFactory;
        private readonly IDbTransaction _transaction;

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