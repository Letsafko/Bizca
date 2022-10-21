namespace Bizca.Core.Application.Test.Pipelines.UnitOfWork
{
    using Cqrs;
    using NFluent;
    using NSubstitute;
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public class UnitOfWorkCommandBehaviorTest
    {
        [Fact]
        public async Task Handle_Next_Return_Ok_CallInOrder_Begin_Invoke_Commit()
        {
            //arrange
            UnitOfWorkCommandBuilder builder = UnitOfWorkCommandBuilder.Instance.DelegateReturnResponse();

            //act
            await builder.Build<FakeCommand2>().Handle(new FakeCommand2(),
                builder.pipelineBehaviourDelegate,
                default).ConfigureAwait(false);

            //assert
            Received.InOrder(() =>
            {
                builder.unitOfWork.Begin();
                builder.pipelineBehaviourDelegate.Invoke();
                builder.unitOfWork.Commit();
            });

            builder.ReceivedCommit(1)
                .DidNotReceivedRollback();
        }

        [Fact]
        public void Handle_Next_Throw_Error_CallInOrder_Begin_Invoke_Rollback()
        {
            //arrange
            UnitOfWorkCommandBuilder builder = UnitOfWorkCommandBuilder.Instance.DelegateThrowException();

            //act & assert
            Check.ThatAsyncCode(() =>
                    builder.Build<FakeCommand2>()
                        .Handle(new FakeCommand2(), default, builder.pipelineBehaviourDelegate))
                .Throws<Exception>();

            Received.InOrder(() =>
            {
                builder.unitOfWork.Begin();
                builder.pipelineBehaviourDelegate.Invoke();
                builder.unitOfWork.Rollback();
            });

            builder.ReceivedRollback(1)
                .DidNotReceivedCommit();
        }
    }
}