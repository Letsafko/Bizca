namespace Bizca.User.Domain.Entities.Address.BusinessCheck.Exceptions
{
    using Bizca.Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;

    [Serializable]
    [ExcludeFromCodeCoverage]
    public sealed class AddressNameIsMandatoryException : DomainException
    {
        public AddressNameIsMandatoryException()
        {
        }

        public AddressNameIsMandatoryException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        public AddressNameIsMandatoryException(string message) : base(message)
        {
        }

        public AddressNameIsMandatoryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public AddressNameIsMandatoryException(string message, string propertyName) : base(message, propertyName)
        {
        }

        public AddressNameIsMandatoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}