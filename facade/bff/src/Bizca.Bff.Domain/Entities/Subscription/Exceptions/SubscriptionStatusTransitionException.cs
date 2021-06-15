namespace Bizca.Bff.Domain.Entities.Subscription.Exceptions
{
    using Bizca.Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;

    [Serializable]
    public sealed class SubscriptionStatusTransitionException : DomainException
    {
        /// <inheritdoc/>
        public SubscriptionStatusTransitionException() : this(default(string))
        {
        }

        /// <inheritdoc/>
        public SubscriptionStatusTransitionException(string message) : base(message)
        {
        }

        /// <inheritdoc/>
        public SubscriptionStatusTransitionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <inheritdoc/>
        public SubscriptionStatusTransitionException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        /// <inheritdoc/>
        public SubscriptionStatusTransitionException(string message, IEnumerable<DomainFailure> errors) : base(message, errors)
        {
        }

        /// <inheritdoc/>
        public SubscriptionStatusTransitionException(string message, IEnumerable<DomainFailure> errors, bool appendDefaultMessage) : base(message, errors, appendDefaultMessage)
        {
        }

        /// <inheritdoc/>
        private SubscriptionStatusTransitionException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}