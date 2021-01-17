namespace Bizca.Core.Application.Test.Pipelines.Validation
{
    using Bizca.Core.Application.Test.Cqrs;
    using FluentValidation;
    using FluentValidation.Results;
    using NFluent;
    using NSubstitute;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public sealed class ValidationBehaviorTest
    {
        [Fact]
        public async Task Handle_Validate_Return_Ok_CallInOrder_GetValidator_Invoke()
        {
            //arrange
            ValidationBehaviorBuilder<FakeCommand, FakeResponse> builder = ValidationBehaviorBuilder<FakeCommand, FakeResponse>.Instance.DelegateReturnResponse(new FakeResponse());

            //act
            await builder.Build().Handle(new FakeCommand(),
                default,
                builder.pipelineBehaviourDelegate).ConfigureAwait(false);

            //assert
            builder.ReceivedDelegate(1);
            Received.InOrder(() =>
            {
                builder.validatorFactory.GetValidator(typeof(FakeCommand));
                builder.pipelineBehaviourDelegate.Invoke();
            });
        }

        [Fact]
        public void Handle_Validate_Throw_Error_CallInOrder_GetValidator()
        {
            //arrange
            IValidator validator = Substitute.For<IValidator>();
            ValidationResult validationResult = GetValidationResult();
            validator.Validate(Arg.Any<ValidationContext<FakeCommand>>()).Returns(validationResult);
            ValidationBehaviorBuilder<FakeCommand, FakeResponse> builder =
                ValidationBehaviorBuilder<FakeCommand, FakeResponse>
                    .Instance
                    .DelegateReturnResponse(new FakeResponse())
                    .GetValidator(validator);

            //act & assert
            Check.ThatAsyncCode(() => builder.Build().Handle(new FakeCommand(), default, builder.pipelineBehaviourDelegate))
                 .Throws<ValidationException>();

            builder.DidNotReceivedDelegate();
            Received.InOrder(() => builder.validatorFactory.GetValidator(typeof(FakeCommand)));
        }

        private ValidationResult GetValidationResult()
        {
            var failure = new ValidationFailure("propertyName", "errormessage");
            return new ValidationResult(new List<ValidationFailure> { failure });
        }
    }
}