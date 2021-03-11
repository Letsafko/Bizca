namespace Bizca.User.Domain.Entities.Address.BusinessCheck.Exceptions
{
    using Bizca.Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [Serializable]
    [ExcludeFromCodeCoverage]
    public sealed class AddressZipCodeIsMandatoryException : DomainException
    {
        /// <inheritdoc/>
        public AddressZipCodeIsMandatoryException() : this(default(string))
        {
        }

        /// <inheritdoc/>
        public AddressZipCodeIsMandatoryException(string message) : base(message)
        {
        }

        /// <inheritdoc/>
        public AddressZipCodeIsMandatoryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <inheritdoc/>
        public AddressZipCodeIsMandatoryException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        /// <inheritdoc/>
        public AddressZipCodeIsMandatoryException(string message, IEnumerable<DomainFailure> errors) : base(message, errors)
        {
        }

        /// <inheritdoc/>
        public AddressZipCodeIsMandatoryException(string message, IEnumerable<DomainFailure> errors, bool appendDefaultMessage) : base(message, errors, appendDefaultMessage)
        {
        }

        /// <inheritdoc/>
        private AddressZipCodeIsMandatoryException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}