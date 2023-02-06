namespace Bizca.Bff.Domain.Entities.Subscription.Exceptions
{
    using Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class UnSupportedSubscriptionStatusException : DomainException
    {
        public UnSupportedSubscriptionStatusException()
        {
        }

        public UnSupportedSubscriptionStatusException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        public UnSupportedSubscriptionStatusException(string message) : base(message)
        {
        }

        public UnSupportedSubscriptionStatusException(string message, Exception innerException) : base(message,
            innerException)
        {
        }

        public UnSupportedSubscriptionStatusException(string message, string propertyName) : base(message, propertyName)
        {
        }

        public UnSupportedSubscriptionStatusException(SerializationInfo info, StreamingContext context) : base(info,
            context)
        {
        }
    }
}