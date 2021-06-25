namespace Bizca.User.Domain.Agregates.BusinessCheck.Exceptions
{
    using Bizca.Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;

    [Serializable]
    [ExcludeFromCodeCoverage]
    public sealed class EmailIsMandatoryException : DomainException
    {
        public EmailIsMandatoryException()
        {
        }

        public EmailIsMandatoryException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        public EmailIsMandatoryException(string message) : base(message)
        {
        }

        public EmailIsMandatoryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public EmailIsMandatoryException(string message, string propertyName) : base(message, propertyName)
        {
        }

        public EmailIsMandatoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}