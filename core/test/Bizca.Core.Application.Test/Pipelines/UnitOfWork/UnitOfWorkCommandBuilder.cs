﻿namespace Bizca.Core.Application.Test.Pipelines.UnitOfWork
{
    using Bizca.Core.Application.Behaviors;
    using Bizca.Core.Application.Commands;
    using Bizca.Core.Application.Services;
    using Bizca.Core.Domain;
    using MediatR;
    using NSubstitute;
    using NSubstitute.ExceptionExtensions;
    using System;

    internal sealed class UnitOfWorkCommandBuilder
    {
        public readonly RequestHandlerDelegate<Unit> pipelineBehaviourDelegate;
        public readonly IEventService eventService;
        public readonly IUnitOfWork unitOfWork;

        private UnitOfWorkCommandBuilder()
        {
            pipelineBehaviourDelegate = Substitute.For<RequestHandlerDelegate<Unit>>();
            eventService = Substitute.For<IEventService>();
            unitOfWork = Substitute.For<IUnitOfWork>();
        }

        internal static UnitOfWorkCommandBuilder Instance => new UnitOfWorkCommandBuilder();
        internal UnitOfWorkCommandBehavior<TCommand> Build<TCommand>() where TCommand : ICommand
        {
            return new UnitOfWorkCommandBehavior<TCommand>(unitOfWork, eventService);
        }

        internal UnitOfWorkCommandBuilder DelegateThrowException()
        {
            pipelineBehaviourDelegate.Invoke().Throws<Exception>();
            return this;
        }

        internal UnitOfWorkCommandBuilder DelegateReturnResponse()
        {
            pipelineBehaviourDelegate.Invoke().Returns(Unit.Value);
            return this;
        }

        internal UnitOfWorkCommandBuilder ReceivedCommit(int numberOfCalls)
        {
            unitOfWork.Received(numberOfCalls).Commit();
            return this;
        }

        internal UnitOfWorkCommandBuilder DidNotReceivedCommit()
        {
            unitOfWork.DidNotReceive().Commit();
            return this;
        }

        internal UnitOfWorkCommandBuilder ReceivedRollback(int numberOfCalls)
        {
            unitOfWork.Received(numberOfCalls).Rollback();
            return this;
        }

        internal UnitOfWorkCommandBuilder DidNotReceivedRollback()
        {
            unitOfWork.DidNotReceive().Rollback();
            return this;
        }
    }
}