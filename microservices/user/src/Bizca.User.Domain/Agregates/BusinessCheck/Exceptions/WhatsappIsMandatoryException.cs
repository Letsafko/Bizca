namespace Bizca.User.Domain.Agregates.BusinessCheck.Exceptions
{
    using Bizca.Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;

    [Serializable]
    [ExcludeFromCodeCoverage]
    public sealed class WhatsappIsMandatoryException : DomainException
    {
        public WhatsappIsMandatoryException()
        {
        }

        public WhatsappIsMandatoryException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        public WhatsappIsMandatoryException(string message) : base(message)
        {
        }

        public WhatsappIsMandatoryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public WhatsappIsMandatoryException(string message, string propertyName) : base(message, propertyName)
        {
        }

        public WhatsappIsMandatoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}