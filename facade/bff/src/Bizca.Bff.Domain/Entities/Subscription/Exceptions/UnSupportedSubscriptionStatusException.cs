namespace Bizca.Bff.Domain.Entities.Subscription.Exceptions
{
    using Bizca.Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;

    [Serializable]
    public sealed class UnSupportedSubscriptionStatusException : DomainException
    {
        /// <inheritdoc/>
        public UnSupportedSubscriptionStatusException() : this(default(string))
        {
        }

        /// <inheritdoc/>
        public UnSupportedSubscriptionStatusException(string message) : base(message)
        {
        }

        /// <inheritdoc/>
        public UnSupportedSubscriptionStatusException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <inheritdoc/>
        public UnSupportedSubscriptionStatusException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        /// <inheritdoc/>
        public UnSupportedSubscriptionStatusException(string message, IEnumerable<DomainFailure> errors) : base(message, errors)
        {
        }

        /// <inheritdoc/>
        public UnSupportedSubscriptionStatusException(string message, IEnumerable<DomainFailure> errors, bool appendDefaultMessage) : base(message, errors, appendDefaultMessage)
        {
        }

        /// <inheritdoc/>
        private UnSupportedSubscriptionStatusException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}
