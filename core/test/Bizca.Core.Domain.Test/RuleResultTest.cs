namespace Bizca.Core.Domain.Test
{
    using Bizca.Core.Domain.Exceptions;
    using NFluent;
    using System;
    using Xunit;

    public sealed class RuleResultTest
    {
        [Fact]
        public void Initialize_ctor_set_properties()
        {
            //arrange
            const bool success = true;
            const string errorMessage = "errorMessage";
            const string propertyName = "propertyName";
            Type exceptionType = typeof(DomainException);
            var failure = new DomainFailure(errorMessage, propertyName, exceptionType);

            //act
            var result = new RuleResult(success, failure);

            //assert
            Check.That(success).Equals(result.Sucess);
            Check.That(errorMessage).Equals(result.Failure.ErrorMessage);
            Check.That(propertyName).Equals(result.Failure.PropertyName);
            Check.That(exceptionType).Equals(result.Failure.ExceptionType);
        }
    }
}