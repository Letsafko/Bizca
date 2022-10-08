namespace Bizca.User.Domain.Agregates.BusinessCheck.Exceptions
{
    using Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;

    [Serializable]
    [ExcludeFromCodeCoverage]
    public sealed class PhoneNumberIsMandatoryException : DomainException
    {
        public PhoneNumberIsMandatoryException()
        {
        }

        public PhoneNumberIsMandatoryException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        public PhoneNumberIsMandatoryException(string message) : base(message)
        {
        }

        public PhoneNumberIsMandatoryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public PhoneNumberIsMandatoryException(string message, string propertyName) : base(message, propertyName)
        {
        }

        public PhoneNumberIsMandatoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}