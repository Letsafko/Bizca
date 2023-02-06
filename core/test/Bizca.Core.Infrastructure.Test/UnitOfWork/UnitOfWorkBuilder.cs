namespace Bizca.Core.Infrastructure.Test.UnitOfWork
{
    using Database;
    using Bizca.Core.Infrastructure.Database.Configuration;
    using Microsoft.Extensions.Logging;
    using NSubstitute;
    using System.Data;

    public sealed class UnitOfWorkBuilder
    {
        private readonly IDbConnection _connection;
        private readonly IConnectionFactory _connectionFactory;
        private readonly ILogger<UnitOfWork> _logger;
        private readonly IDbTransaction _transaction;

        private UnitOfWorkBuilder()
        {
            _connectionFactory = Substitute.For<IConnectionFactory>();
            _transaction = Substitute.For<IDbTransaction>();
            _logger = Substitute.For<ILogger<UnitOfWork>>();
            _connection = Substitute.For<IDbConnection>();
        }

        public static UnitOfWorkBuilder Instance => new UnitOfWorkBuilder();

        public UnitOfWork Build()
        {
            return new UnitOfWork(_connectionFactory, _logger);
        }

        public UnitOfWorkBuilder WithConnectionFactory<T>() where T : class, IDatabaseConfiguration, new()
        {
            _connectionFactory
                .CreateConnection()
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