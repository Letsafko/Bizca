namespace Bizca.Bff.Domain.Entities.Subscription.Exceptions
{
    using Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class SubscriptionStatusTransitionException : DomainException
    {
        public SubscriptionStatusTransitionException()
        {
        }

        public SubscriptionStatusTransitionException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        public SubscriptionStatusTransitionException(string message) : base(message)
        {
        }

        public SubscriptionStatusTransitionException(string message, Exception innerException) : base(message,
            innerException)
        {
        }

        public SubscriptionStatusTransitionException(string message, string propertyName) : base(message, propertyName)
        {
        }

        public SubscriptionStatusTransitionException(SerializationInfo info, StreamingContext context) : base(info,
            context)
        {
        }
    }
}