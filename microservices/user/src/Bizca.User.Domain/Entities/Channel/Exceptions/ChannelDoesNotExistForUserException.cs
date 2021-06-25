namespace Bizca.User.Domain.Entities.Channel.Exceptions
{
    using Bizca.Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;

    [Serializable]
    [ExcludeFromCodeCoverage]
    public sealed class ChannelDoesNotExistForUserException : ResourceNotFoundException
    {
        public ChannelDoesNotExistForUserException()
        {
        }

        public ChannelDoesNotExistForUserException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        public ChannelDoesNotExistForUserException(string message) : base(message)
        {
        }

        public ChannelDoesNotExistForUserException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ChannelDoesNotExistForUserException(string message, string propertyName) : base(message, propertyName)
        {
        }

        public ChannelDoesNotExistForUserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}