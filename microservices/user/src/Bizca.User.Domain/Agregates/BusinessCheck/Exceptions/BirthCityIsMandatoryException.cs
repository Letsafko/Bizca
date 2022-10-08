namespace Bizca.User.Domain.Agregates.BusinessCheck.Exceptions
{
    using Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;

    [Serializable]
    [ExcludeFromCodeCoverage]
    public sealed class BirthCityIsMandatoryException : DomainException
    {
        public BirthCityIsMandatoryException()
        {
        }

        public BirthCityIsMandatoryException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        public BirthCityIsMandatoryException(string message) : base(message)
        {
        }

        public BirthCityIsMandatoryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public BirthCityIsMandatoryException(string message, string propertyName) : base(message, propertyName)
        {
        }

        public BirthCityIsMandatoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}