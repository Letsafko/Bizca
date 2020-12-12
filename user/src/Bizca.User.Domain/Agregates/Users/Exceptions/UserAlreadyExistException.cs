namespace Bizca.User.Domain.Agregates.Users.Exceptions
{
    using Bizca.Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [Serializable]
    [ExcludeFromCodeCoverage]
    public sealed class UserAlreadyExistException : DomainException
    {
        /// <inheritdoc/>
        public UserAlreadyExistException() : this(default(string))
        {
        }

        /// <inheritdoc/>
        public UserAlreadyExistException(string message) : base(message)
        {
        }

        /// <inheritdoc/>
        public UserAlreadyExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <inheritdoc/>
        public UserAlreadyExistException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        /// <inheritdoc/>
        public UserAlreadyExistException(string message, IEnumerable<DomainFailure> errors) : base(message, errors)
        {
        }

        /// <inheritdoc/>
        public UserAlreadyExistException(string message, IEnumerable<DomainFailure> errors, bool appendDefaultMessage) : base(message, errors, appendDefaultMessage)
        {
        }

        /// <inheritdoc/>
        private UserAlreadyExistException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}
