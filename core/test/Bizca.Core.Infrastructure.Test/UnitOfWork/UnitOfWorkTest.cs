namespace Bizca.Core.Infrastructure.Test
{
    using Bizca.Core.Infrastructure;
    using Bizca.Core.Infrastructure.Abstracts.Configuration;
    using NFluent;
    using NSubstitute;
    using System;
    using System.Data;
    using Xunit;

    public sealed class UnitOfWorkTest
    {
        private readonly IDbConnection connection;
        private readonly IDbTransaction transaction;
        public UnitOfWorkTest()
        {
            connection = Substitute.For<IDbConnection>();
            transaction = Substitute.For<IDbTransaction>();
        }

        [Fact]
        public void Ctor_NullArgument_throw_argumentnullexception()
        {
            Check.ThatCode(() => UnitOfWorkBuilder.Instance.WithConnectionFactory(default).Build())
                 .Throws<ArgumentNullException>();
        }

        [Fact]
        public void Begin_ShouldCallOnce_BeginTransaction()
        {
            //arrange
            connection.BeginTransaction(IsolationLevel.ReadCommitted).Returns(transaction);
            UnitOfWork unitOfWork = UnitOfWorkBuilder.Instance
                .WithDbConnection<BizcaDatabaseConfiguration>(connection)
                .Build();

            //act
            unitOfWork.Begin();

            //assert
            Check.That(unitOfWork.Connection).Equals(connection);
            Check.That(unitOfWork.Transaction).Equals(transaction);
            unitOfWork.Connection.Received(1).BeginTransaction(IsolationLevel.ReadCommitted);
        }

        [Fact]
        public void Commit_ShouldCallOnce_CommitMethodAndResetConnection()
        {
            //arrange
            connection.BeginTransaction(IsolationLevel.ReadCommitted).Returns(transaction);
            UnitOfWork unitOfWork = UnitOfWorkBuilder.Instance
                .WithDbConnection<BizcaDatabaseConfiguration>(connection)
                .Build();

            //act
            unitOfWork.Begin();
            unitOfWork.Commit();

            //assert
            transaction.Received(1).Commit();
            transaction.Received(1).Dispose();
            Check.That(unitOfWork.Connection).IsNull();
            Check.That(unitOfWork.Transaction).IsNull();
            Received.InOrder(() =>
            {
                connection.BeginTransaction(IsolationLevel.ReadCommitted);
                transaction.Commit();
                unitOfWork.Dispose();
            });
        }

        [Fact]
        public void Rollback_ShouldCallOnce_RollbackMethodAndResetConnection()
        {
            //arrange
            connection.BeginTransaction(IsolationLevel.ReadCommitted).Returns(transaction);
            UnitOfWork unitOfWork = UnitOfWorkBuilder.Instance
                .WithDbConnection<BizcaDatabaseConfiguration>(connection)
                .Build();

            //act
            unitOfWork.Begin();
            unitOfWork.Rollback();

            //assert
            transaction.Received(1).Dispose();
            transaction.Received(1).Rollback();
            Check.That(unitOfWork.Connection).IsNull();
            Check.That(unitOfWork.Transaction).IsNull();
            Received.InOrder(() =>
            {
                connection.BeginTransaction(IsolationLevel.ReadCommitted);
                transaction.Rollback();
                unitOfWork.Dispose();
            });
        }
    }
}