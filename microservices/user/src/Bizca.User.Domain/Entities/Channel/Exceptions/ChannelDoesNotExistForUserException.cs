namespace Bizca.User.Domain.Entities.Channel.Exceptions
{
    using Bizca.Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [Serializable]
    [ExcludeFromCodeCoverage]
    public sealed class ChannelDoesNotExistForUserException : NotFoundDomainException
    {
        /// <inheritdoc/>
        public ChannelDoesNotExistForUserException() : this(default(string))
        {
        }

        /// <inheritdoc/>
        public ChannelDoesNotExistForUserException(string message) : base(message)
        {
        }

        /// <inheritdoc/>
        public ChannelDoesNotExistForUserException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <inheritdoc/>
        public ChannelDoesNotExistForUserException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        /// <inheritdoc/>
        public ChannelDoesNotExistForUserException(string message, IEnumerable<DomainFailure> errors) : base(message, errors)
        {
        }

        /// <inheritdoc/>
        public ChannelDoesNotExistForUserException(string message, IEnumerable<DomainFailure> errors, bool appendDefaultMessage) : base(message, errors, appendDefaultMessage)
        {
        }

        /// <inheritdoc/>
        private ChannelDoesNotExistForUserException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}