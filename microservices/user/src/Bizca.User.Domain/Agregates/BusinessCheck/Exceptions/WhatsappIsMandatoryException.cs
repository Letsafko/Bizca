namespace Bizca.User.Domain.Agregates.BusinessCheck.Exceptions
{
    using Bizca.Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [Serializable]
    [ExcludeFromCodeCoverage]
    public sealed class WhatsappIsMandatoryException : DomainException
    {
        /// <inheritdoc/>
        public WhatsappIsMandatoryException() : this(default(string))
        {
        }

        /// <inheritdoc/>
        public WhatsappIsMandatoryException(string message) : base(message)
        {
        }

        /// <inheritdoc/>
        public WhatsappIsMandatoryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <inheritdoc/>
        public WhatsappIsMandatoryException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        /// <inheritdoc/>
        public WhatsappIsMandatoryException(string message, IEnumerable<DomainFailure> errors) : base(message, errors)
        {
        }

        /// <inheritdoc/>
        public WhatsappIsMandatoryException(string message, IEnumerable<DomainFailure> errors, bool appendDefaultMessage) : base(message, errors, appendDefaultMessage)
        {
        }

        /// <inheritdoc/>
        private WhatsappIsMandatoryException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}