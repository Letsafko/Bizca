namespace Bizca.User.Domain.Agregates.BusinessCheck.Exceptions
{
    using Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;

    [Serializable]
    [ExcludeFromCodeCoverage]
    public sealed class AddressIsMandatoryException : DomainException
    {
        public AddressIsMandatoryException()
        {
        }

        public AddressIsMandatoryException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        public AddressIsMandatoryException(string message) : base(message)
        {
        }

        public AddressIsMandatoryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public AddressIsMandatoryException(string message, string propertyName) : base(message, propertyName)
        {
        }

        public AddressIsMandatoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}