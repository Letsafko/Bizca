namespace Bizca.User.Domain.Entities.Channel.Exceptions
{
    using Bizca.Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;

    [Serializable]
    [ExcludeFromCodeCoverage]
    public sealed class ChannelCodeConfirmationHasExpiredUserException : DomainException
    {
        public ChannelCodeConfirmationHasExpiredUserException()
        {
        }

        public ChannelCodeConfirmationHasExpiredUserException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        public ChannelCodeConfirmationHasExpiredUserException(string message) : base(message)
        {
        }

        public ChannelCodeConfirmationHasExpiredUserException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ChannelCodeConfirmationHasExpiredUserException(string message, string propertyName) : base(message, propertyName)
        {
        }

        public ChannelCodeConfirmationHasExpiredUserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}