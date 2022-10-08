namespace Bizca.User.Domain.Entities.Channel.Exceptions
{
    using Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;

    [Serializable]
    [ExcludeFromCodeCoverage]
    public sealed class ChannelCodeConfirmationDoesNotExistException : ResourceNotFoundException
    {
        public ChannelCodeConfirmationDoesNotExistException()
        {
        }

        public ChannelCodeConfirmationDoesNotExistException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        public ChannelCodeConfirmationDoesNotExistException(string message) : base(message)
        {
        }

        public ChannelCodeConfirmationDoesNotExistException(string message, Exception innerException) : base(message,
            innerException)
        {
        }

        public ChannelCodeConfirmationDoesNotExistException(string message, string propertyName) : base(message,
            propertyName)
        {
        }

        public ChannelCodeConfirmationDoesNotExistException(SerializationInfo info, StreamingContext context) : base(
            info, context)
        {
        }
    }
}