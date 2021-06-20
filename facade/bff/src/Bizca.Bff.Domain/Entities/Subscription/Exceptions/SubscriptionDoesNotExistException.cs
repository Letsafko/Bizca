namespace Bizca.Bff.Domain.Entities.Subscription.Exceptions
{
    using Bizca.Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;

    [Serializable]
    public sealed class SubscriptionDoesNotExistException : NotFoundDomainException
    {
        /// <inheritdoc/>
        public SubscriptionDoesNotExistException() : this(default(string))
        {
        }

        /// <inheritdoc/>
        public SubscriptionDoesNotExistException(string message) : base(message)
        {
        }

        /// <inheritdoc/>
        public SubscriptionDoesNotExistException(string propertyName, string message) : base(propertyName, message)
        {
        }

        /// <inheritdoc/>
        public SubscriptionDoesNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <inheritdoc/>
        public SubscriptionDoesNotExistException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        /// <inheritdoc/>
        public SubscriptionDoesNotExistException(string message, IEnumerable<DomainFailure> errors) : base(message, errors)
        {
        }

        /// <inheritdoc/>
        public SubscriptionDoesNotExistException(string message, IEnumerable<DomainFailure> errors, bool appendDefaultMessage) : base(message, errors, appendDefaultMessage)
        {
        }

        /// <inheritdoc/>
        private SubscriptionDoesNotExistException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}