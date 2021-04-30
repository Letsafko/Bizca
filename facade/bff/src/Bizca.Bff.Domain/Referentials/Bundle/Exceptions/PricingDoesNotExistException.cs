namespace Bizca.Bff.Domain.Referentials.Pricing.Exceptions
{
    using Bizca.Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;

    [Serializable]
    public sealed class PricingDoesNotExistException : NotFoundDomainException
    {
        /// <inheritdoc/>
        public PricingDoesNotExistException() : this(default(string))
        {
        }

        /// <inheritdoc/>
        public PricingDoesNotExistException(string message) : base(message)
        {
        }

        /// <inheritdoc/>
        public PricingDoesNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <inheritdoc/>
        public PricingDoesNotExistException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        /// <inheritdoc/>
        public PricingDoesNotExistException(string message, IEnumerable<DomainFailure> errors) : base(message, errors)
        {
        }

        /// <inheritdoc/>
        public PricingDoesNotExistException(string message, IEnumerable<DomainFailure> errors, bool appendDefaultMessage) : base(message, errors, appendDefaultMessage)
        {
        }

        /// <inheritdoc/>
        private PricingDoesNotExistException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}
