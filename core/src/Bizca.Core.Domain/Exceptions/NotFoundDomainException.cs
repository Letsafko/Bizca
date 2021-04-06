namespace Bizca.Core.Domain.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    ///     an exception that represents notfound business error.
    /// </summary>
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class NotFoundDomainException : DomainException
    {
        /// <inheritdoc/>
        public NotFoundDomainException() : this(default(string))
        {
        }

        /// <inheritdoc/>
        public NotFoundDomainException(string message) : base(message)
        {
        }

        /// <inheritdoc/>
        public NotFoundDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <inheritdoc/>
        public NotFoundDomainException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        /// <inheritdoc/>
        public NotFoundDomainException(string message, IEnumerable<DomainFailure> errors) : base(message, errors)
        {
        }

        /// <inheritdoc/>
        public NotFoundDomainException(string message, IEnumerable<DomainFailure> errors, bool appendDefaultMessage) : base(message, errors, appendDefaultMessage)
        {
        }

        /// <inheritdoc/>
        protected NotFoundDomainException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}