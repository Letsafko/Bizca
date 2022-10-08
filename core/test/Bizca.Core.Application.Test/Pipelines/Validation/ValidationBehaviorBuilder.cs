namespace Bizca.Core.Application.Test.Pipelines.Validation
{
    using Behaviors;
    using FluentValidation;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using NSubstitute;
    using System;

    internal sealed class ValidationBehaviorBuilder<TRequest, TResponse>
    {
        public readonly ILogger<ValidationBehavior<TRequest, TResponse>> logger;
        public readonly RequestHandlerDelegate<TResponse> pipelineBehaviourDelegate;
        public readonly IValidatorFactory validatorFactory;

        private ValidationBehaviorBuilder()
        {
            validatorFactory = Substitute.For<IValidatorFactory>();
            logger = Substitute.For<ILogger<ValidationBehavior<TRequest, TResponse>>>();
            pipelineBehaviourDelegate = Substitute.For<RequestHandlerDelegate<TResponse>>();
        }

        internal static ValidationBehaviorBuilder<TRequest, TResponse> Instance =>
            new ValidationBehaviorBuilder<TRequest, TResponse>();

        internal ValidationBehavior<TRequest, TResponse> Build()
        {
            return new ValidationBehavior<TRequest, TResponse>(validatorFactory, logger);
        }

        internal ValidationBehaviorBuilder<TRequest, TResponse> GetValidator(IValidator validator)
        {
            validatorFactory.GetValidator(Arg.Any<Type>()).Returns(validator);
            return this;
        }

        internal ValidationBehaviorBuilder<TRequest, TResponse> DelegateReturnResponse(TResponse response)
        {
            pipelineBehaviourDelegate.Invoke().Returns(response);
            return this;
        }

        internal ValidationBehaviorBuilder<TRequest, TResponse> ReceivedDelegate(int numberOfCalls)
        {
            pipelineBehaviourDelegate.Received(numberOfCalls).Invoke();
            return this;
        }

        internal ValidationBehaviorBuilder<TRequest, TResponse> DidNotReceivedDelegate()
        {
            pipelineBehaviourDelegate.DidNotReceive().Invoke();
            return this;
        }
    }
}