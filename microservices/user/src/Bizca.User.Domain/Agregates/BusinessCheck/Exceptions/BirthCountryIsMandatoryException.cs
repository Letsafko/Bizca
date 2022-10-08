namespace Bizca.User.Domain.Agregates.BusinessCheck.Exceptions
{
    using Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;

    [Serializable]
    [ExcludeFromCodeCoverage]
    public sealed class BirthCountryIsMandatoryException : DomainException
    {
        public BirthCountryIsMandatoryException()
        {
        }

        public BirthCountryIsMandatoryException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        public BirthCountryIsMandatoryException(string message) : base(message)
        {
        }

        public BirthCountryIsMandatoryException(string message, Exception innerException) : base(message,
            innerException)
        {
        }

        public BirthCountryIsMandatoryException(string message, string propertyName) : base(message, propertyName)
        {
        }

        public BirthCountryIsMandatoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}