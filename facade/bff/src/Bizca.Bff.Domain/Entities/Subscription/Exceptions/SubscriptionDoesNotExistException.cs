namespace Bizca.Bff.Domain.Entities.Subscription.Exceptions
{
    using Bizca.Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class SubscriptionDoesNotExistException : ResourceNotFoundException
    {
        public SubscriptionDoesNotExistException()
        {
        }

        public SubscriptionDoesNotExistException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        public SubscriptionDoesNotExistException(string message) : base(message)
        {
        }

        public SubscriptionDoesNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public SubscriptionDoesNotExistException(string message, string propertyName) : base(message, propertyName)
        {
        }

        public SubscriptionDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}