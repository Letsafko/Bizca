namespace Bizca.Core.Domain.Test.Exceptions
{
    using Bizca.Core.Domain.Exceptions;
    using NFluent;
    using System.Collections.Generic;
    using Xunit;

    public sealed class DomainExceptionTest
    {
        private const string errorCode = "1236";
        private const string propertyName = "property";
        private const string expected = "custom message";
        private const Severity severity = Severity.Error;

        private const string errorMessage = "error message";
        private readonly IEnumerable<DomainFailure> erros = new List<DomainFailure> { new DomainFailure(propertyName, errorMessage)
        {
            Severity = severity,
            ErrorCode = errorCode
        }};

        [Fact]
        public void Ctor_argumentless_return_defaultmessage()
        {
            //arrange
            var exception = new DomainException();
            string expected = $"Exception of type '{exception.GetType().FullName}' was thrown.";

            //assert
            Check.That(exception.Errors).IsEmpty();
            Check.That(exception.Message).IsEqualIgnoringCase(expected);
            Check.That(exception.Errors).InheritsFrom<IEnumerable<DomainFailure>>();
        }

        [Fact]
        public void Ctor_domainfailures_init_message_and_errors()
        {
            //act
            var exception = new DomainException(expected, erros);

            //assert
            Check.That(exception.Errors).CountIs(1);
            Check.That(exception.Message).Equals(expected);
            Check.That(exception.Errors).HasFieldsWithSameValues(erros);
            Check.That(exception.Errors).InheritsFrom<IEnumerable<DomainFailure>>();
        }

        [Fact]
        public void Ctor_domainfailures_init_errors()
        {
            //act
            var exception = new DomainException(erros);

            //assert  
            Check.That(exception.Errors).CountIs(1);
            Check.That(exception.Errors).HasFieldsWithSameValues(erros);
            Check.That(exception.Message).Equals($"\r\n -- {propertyName}: {errorMessage}");
            Check.That(exception.Errors).InheritsFrom<IEnumerable<DomainFailure>>();
        }

        [Fact]
        public void Ctor_message_and_exception_init_message_and_innerexception()
        {
            //act
            var expectedInnerException = new System.Exception();
            var exception = new DomainException(expected, expectedInnerException);

            //assert  
            Check.That(exception.Message).Equals(expected);
            Check.That(exception.InnerException).HasFieldsWithSameValues(expectedInnerException);
        }
    }
}