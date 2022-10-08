namespace Bizca.User.Domain.Entities.Address.BusinessCheck.Exceptions
{
    using Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;

    [Serializable]
    [ExcludeFromCodeCoverage]
    public sealed class AddressZipCodeIsMandatoryException : DomainException
    {
        public AddressZipCodeIsMandatoryException()
        {
        }

        public AddressZipCodeIsMandatoryException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        public AddressZipCodeIsMandatoryException(string message) : base(message)
        {
        }

        public AddressZipCodeIsMandatoryException(string message, Exception innerException) : base(message,
            innerException)
        {
        }

        public AddressZipCodeIsMandatoryException(string message, string propertyName) : base(message, propertyName)
        {
        }

        public AddressZipCodeIsMandatoryException(SerializationInfo info, StreamingContext context) : base(info,
            context)
        {
        }
    }
}