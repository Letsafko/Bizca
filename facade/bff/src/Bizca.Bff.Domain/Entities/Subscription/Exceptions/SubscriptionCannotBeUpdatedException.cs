namespace Bizca.Bff.Domain.Entities.Subscription.Exceptions
{
    using Bizca.Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;

    [Serializable]
    public sealed class SubscriptionCannotBeUpdatedException : DomainException
    {
        /// <inheritdoc/>
        public SubscriptionCannotBeUpdatedException() : this(default(string))
        {
        }

        /// <inheritdoc/>
        public SubscriptionCannotBeUpdatedException(string message) : base(message)
        {
        }

        /// <inheritdoc/>
        public SubscriptionCannotBeUpdatedException(string propertyName, string message) : base(propertyName, message)
        {
        }

        /// <inheritdoc/>
        public SubscriptionCannotBeUpdatedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <inheritdoc/>
        public SubscriptionCannotBeUpdatedException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        /// <inheritdoc/>
        public SubscriptionCannotBeUpdatedException(string message, IEnumerable<DomainFailure> errors) : base(message, errors)
        {
        }

        /// <inheritdoc/>
        public SubscriptionCannotBeUpdatedException(string message, IEnumerable<DomainFailure> errors, bool appendDefaultMessage) : base(message, errors, appendDefaultMessage)
        {
        }

        /// <inheritdoc/>
        private SubscriptionCannotBeUpdatedException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }        
    }
}
