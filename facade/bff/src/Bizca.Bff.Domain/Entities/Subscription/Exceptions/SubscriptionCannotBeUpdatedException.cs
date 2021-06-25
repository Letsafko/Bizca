namespace Bizca.Bff.Domain.Entities.Subscription.Exceptions
{
    using Bizca.Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class SubscriptionCannotBeUpdatedException : DomainException
    {
        public SubscriptionCannotBeUpdatedException()
        {
        }

        public SubscriptionCannotBeUpdatedException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        public SubscriptionCannotBeUpdatedException(string message) : base(message)
        {
        }

        public SubscriptionCannotBeUpdatedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public SubscriptionCannotBeUpdatedException(string message, string propertyName) : base(message, propertyName)
        {
        }

        public SubscriptionCannotBeUpdatedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}