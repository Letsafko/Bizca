namespace Bizca.Bff.Domain.Referentials.Bundle.Exceptions
{
    using Bizca.Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;

    [Serializable]
    public sealed class BundleDoesNotExistException : NotFoundDomainException
    {
        /// <inheritdoc/>
        public BundleDoesNotExistException() : this(default(string))
        {
        }

        /// <inheritdoc/>
        public BundleDoesNotExistException(string message) : base(message)
        {
        }

        /// <inheritdoc/>
        public BundleDoesNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <inheritdoc/>
        public BundleDoesNotExistException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        /// <inheritdoc/>
        public BundleDoesNotExistException(string message, IEnumerable<DomainFailure> errors) : base(message, errors)
        {
        }

        /// <inheritdoc/>
        public BundleDoesNotExistException(string message, IEnumerable<DomainFailure> errors, bool appendDefaultMessage) : base(message, errors, appendDefaultMessage)
        {
        }

        /// <inheritdoc/>
        private BundleDoesNotExistException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}
