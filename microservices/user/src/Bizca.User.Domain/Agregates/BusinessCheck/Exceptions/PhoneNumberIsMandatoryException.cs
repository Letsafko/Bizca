namespace Bizca.User.Domain.Agregates.BusinessCheck.Exceptions
{
    using Bizca.Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [Serializable]
    [ExcludeFromCodeCoverage]
    public sealed class PhoneNumberIsMandatoryException : DomainException
    {
        /// <inheritdoc/>
        public PhoneNumberIsMandatoryException() : this(default(string))
        {
        }

        /// <inheritdoc/>
        public PhoneNumberIsMandatoryException(string message) : base(message)
        {
        }

        /// <inheritdoc/>
        public PhoneNumberIsMandatoryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <inheritdoc/>
        public PhoneNumberIsMandatoryException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        /// <inheritdoc/>
        public PhoneNumberIsMandatoryException(string message, IEnumerable<DomainFailure> errors) : base(message, errors)
        {
        }

        /// <inheritdoc/>
        public PhoneNumberIsMandatoryException(string message, IEnumerable<DomainFailure> errors, bool appendDefaultMessage) : base(message, errors, appendDefaultMessage)
        {
        }

        /// <inheritdoc/>
        private PhoneNumberIsMandatoryException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}