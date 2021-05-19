namespace Bizca.Bff.Domain.Entities.User.Exceptions
{
    using Bizca.Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;

    public sealed class UserDoesNotExistException : NotFoundDomainException
    {
        /// <inheritdoc/>
        public UserDoesNotExistException() : this(default(string))
        {
        }

        /// <inheritdoc/>
        public UserDoesNotExistException(string message) : base(message)
        {
        }

        /// <inheritdoc/>
        public UserDoesNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <inheritdoc/>
        public UserDoesNotExistException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        /// <inheritdoc/>
        public UserDoesNotExistException(string message, IEnumerable<DomainFailure> errors) : base(message, errors)
        {
        }

        /// <inheritdoc/>
        public UserDoesNotExistException(string message, IEnumerable<DomainFailure> errors, bool appendDefaultMessage) : base(message, errors, appendDefaultMessage)
        {
        }

        /// <inheritdoc/>
        private UserDoesNotExistException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}
