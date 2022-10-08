namespace Bizca.User.Domain.Entities.Address.BusinessCheck.Exceptions
{
    using Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;

    [Serializable]
    [ExcludeFromCodeCoverage]
    public sealed class AddressStreetIsMandatoryException : DomainException
    {
        public AddressStreetIsMandatoryException()
        {
        }

        public AddressStreetIsMandatoryException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        public AddressStreetIsMandatoryException(string message) : base(message)
        {
        }

        public AddressStreetIsMandatoryException(string message, Exception innerException) : base(message,
            innerException)
        {
        }

        public AddressStreetIsMandatoryException(string message, string propertyName) : base(message, propertyName)
        {
        }

        public AddressStreetIsMandatoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}