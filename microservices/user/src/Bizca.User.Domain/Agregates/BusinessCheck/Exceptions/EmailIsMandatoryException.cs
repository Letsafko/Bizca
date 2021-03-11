namespace Bizca.User.Domain.Agregates.BusinessCheck.Exceptions
{
    using Bizca.Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [Serializable]
    [ExcludeFromCodeCoverage]
    public sealed class EmailIsMandatoryException : DomainException
    {
        /// <inheritdoc/>
        public EmailIsMandatoryException() : this(default(string))
        {
        }

        /// <inheritdoc/>
        public EmailIsMandatoryException(string message) : base(message)
        {
        }

        /// <inheritdoc/>
        public EmailIsMandatoryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <inheritdoc/>
        public EmailIsMandatoryException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        /// <inheritdoc/>
        public EmailIsMandatoryException(string message, IEnumerable<DomainFailure> errors) : base(message, errors)
        {
        }

        /// <inheritdoc/>
        public EmailIsMandatoryException(string message, IEnumerable<DomainFailure> errors, bool appendDefaultMessage) : base(message, errors, appendDefaultMessage)
        {
        }

        /// <inheritdoc/>
        private EmailIsMandatoryException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}