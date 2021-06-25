namespace Bizca.User.Domain.Agregates.BusinessCheck.Exceptions
{
    using Bizca.Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;

    [Serializable]
    [ExcludeFromCodeCoverage]
    public sealed class ChannelResourceMustBeUniqueException : DomainException
    {
        public ChannelResourceMustBeUniqueException()
        {
        }

        public ChannelResourceMustBeUniqueException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        public ChannelResourceMustBeUniqueException(string message) : base(message)
        {
        }

        public ChannelResourceMustBeUniqueException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ChannelResourceMustBeUniqueException(string message, string propertyName) : base(message, propertyName)
        {
        }

        public ChannelResourceMustBeUniqueException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}