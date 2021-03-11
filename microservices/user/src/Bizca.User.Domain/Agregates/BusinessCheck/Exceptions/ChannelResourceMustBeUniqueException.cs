namespace Bizca.User.Domain.Agregates.BusinessCheck.Exceptions
{
    using Bizca.Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [Serializable]
    [ExcludeFromCodeCoverage]
    public sealed class ChannelResourceMustBeUniqueException : DomainException
    {
        /// <inheritdoc/>
        public ChannelResourceMustBeUniqueException() : this(default(string))
        {
        }

        /// <inheritdoc/>
        public ChannelResourceMustBeUniqueException(string message) : base(message)
        {
        }

        /// <inheritdoc/>
        public ChannelResourceMustBeUniqueException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <inheritdoc/>
        public ChannelResourceMustBeUniqueException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        /// <inheritdoc/>
        public ChannelResourceMustBeUniqueException(string message, IEnumerable<DomainFailure> errors) : base(message, errors)
        {
        }

        /// <inheritdoc/>
        public ChannelResourceMustBeUniqueException(string message, IEnumerable<DomainFailure> errors, bool appendDefaultMessage) : base(message, errors, appendDefaultMessage)
        {
        }

        /// <inheritdoc/>
        private ChannelResourceMustBeUniqueException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}