namespace Bizca.User.Domain.Agregates.BusinessCheck.Exceptions
{
    using Bizca.Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;

    [Serializable]
    [ExcludeFromCodeCoverage]
    public sealed class BirthDateIsMandatoryException : DomainException
    {
        public BirthDateIsMandatoryException()
        {
        }

        public BirthDateIsMandatoryException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        public BirthDateIsMandatoryException(string message) : base(message)
        {
        }

        public BirthDateIsMandatoryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public BirthDateIsMandatoryException(string message, string propertyName) : base(message, propertyName)
        {
        }

        public BirthDateIsMandatoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}