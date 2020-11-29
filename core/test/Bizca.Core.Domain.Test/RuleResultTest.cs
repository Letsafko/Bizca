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
            const string message = "message";
            Type exceptionType = typeof(DomainException); 

            //act
            var result = new RuleResult(success, message, exceptionType);

            //assert
            Check.That(success).Equals(result.Sucess);
            Check.That(message).Equals(result.Message);
            Check.That(exceptionType).Equals(result.ExceptionType);
        }
    }
}
