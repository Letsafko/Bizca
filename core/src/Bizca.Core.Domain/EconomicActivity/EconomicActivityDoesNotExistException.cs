namespace Bizca.Core.Domain.EconomicActivity
{
    using Bizca.Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [Serializable]
    [ExcludeFromCodeCoverage]
    public sealed class EconomicActivityDoesNotExistException : DomainException
    {
        /// <inheritdoc/>
        public EconomicActivityDoesNotExistException() : this(default(string))
        {
        }

        /// <inheritdoc/>
        public EconomicActivityDoesNotExistException(string message) : base(message)
        {
        }

        /// <inheritdoc/>
        public EconomicActivityDoesNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <inheritdoc/>
        public EconomicActivityDoesNotExistException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        /// <inheritdoc/>
        public EconomicActivityDoesNotExistException(string message, IEnumerable<DomainFailure> errors) : base(message, errors)
        {
        }

        /// <inheritdoc/>
        public EconomicActivityDoesNotExistException(string message, IEnumerable<DomainFailure> errors, bool appendDefaultMessage) : base(message, errors, appendDefaultMessage)
        {
        }

        /// <inheritdoc/>
        private EconomicActivityDoesNotExistException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}