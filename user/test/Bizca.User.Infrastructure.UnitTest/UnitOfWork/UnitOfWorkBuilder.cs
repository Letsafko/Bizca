namespace Bizca.User.Infrastructure.UnitTest.UnitOfWork
{
    using Bizca.Core.Infrastructure.Abstracts;
    using Bizca.Core.Infrastructure.Abstracts.Configuration;
    using NSubstitute;
    using System.Data;
    using UnitOfWork = Core.Infrastructure.UnitOfWork;

    public sealed class UnitOfWorkBuilder
    {
        private IConnectionFactory _connectionFactory;
        private UnitOfWorkBuilder()
        {
            _connectionFactory = Substitute.For<IConnectionFactory>();
        }

        public static UnitOfWorkBuilder Instance => new UnitOfWorkBuilder();
        public UnitOfWork Build()
        {
            return new UnitOfWork(_connectionFactory);
        }

        public UnitOfWorkBuilder WithConnectionFactory(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            return this;
        }

        public UnitOfWorkBuilder WithDbConnection<T>(IDbConnection connection) where T : class, IDatabaseConfiguration, new()
        {
            _connectionFactory.CreateConnection<T>().Returns(connection);
            return this;
        }
    }
}