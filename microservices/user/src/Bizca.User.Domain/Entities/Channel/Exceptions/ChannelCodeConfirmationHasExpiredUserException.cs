namespace Bizca.User.Domain.Entities.Channel.Exceptions
{
    using Bizca.Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [Serializable]
    [ExcludeFromCodeCoverage]
    public sealed class ChannelCodeConfirmationHasExpiredUserException : DomainException
    {
        /// <inheritdoc/>
        public ChannelCodeConfirmationHasExpiredUserException() : this(default(string))
        {
        }

        /// <inheritdoc/>
        public ChannelCodeConfirmationHasExpiredUserException(string message) : base(message)
        {
        }

        /// <inheritdoc/>
        public ChannelCodeConfirmationHasExpiredUserException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <inheritdoc/>
        public ChannelCodeConfirmationHasExpiredUserException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        /// <inheritdoc/>
        public ChannelCodeConfirmationHasExpiredUserException(string message, IEnumerable<DomainFailure> errors) : base(message, errors)
        {
        }

        /// <inheritdoc/>
        public ChannelCodeConfirmationHasExpiredUserException(string message, IEnumerable<DomainFailure> errors, bool appendDefaultMessage) : base(message, errors, appendDefaultMessage)
        {
        }

        /// <inheritdoc/>
        private ChannelCodeConfirmationHasExpiredUserException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}