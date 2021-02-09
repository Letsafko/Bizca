namespace Bizca.User.Domain.Entities.Channel.Exceptions
{
    using Bizca.Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [Serializable]
    [ExcludeFromCodeCoverage]
    public sealed class ChannelCodeConfirmationDoesNotExistException : DomainException
    {
        /// <inheritdoc/>
        public ChannelCodeConfirmationDoesNotExistException() : this(default(string))
        {
        }

        /// <inheritdoc/>
        public ChannelCodeConfirmationDoesNotExistException(string message) : base(message)
        {
        }

        /// <inheritdoc/>
        public ChannelCodeConfirmationDoesNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <inheritdoc/>
        public ChannelCodeConfirmationDoesNotExistException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        /// <inheritdoc/>
        public ChannelCodeConfirmationDoesNotExistException(string message, IEnumerable<DomainFailure> errors) : base(message, errors)
        {
        }

        /// <inheritdoc/>
        public ChannelCodeConfirmationDoesNotExistException(string message, IEnumerable<DomainFailure> errors, bool appendDefaultMessage) : base(message, errors, appendDefaultMessage)
        {
        }

        /// <inheritdoc/>
        private ChannelCodeConfirmationDoesNotExistException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}