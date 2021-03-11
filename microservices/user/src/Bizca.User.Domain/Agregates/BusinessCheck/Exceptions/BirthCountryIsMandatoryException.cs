namespace Bizca.User.Domain.Agregates.BusinessCheck.Exceptions
{
    using Bizca.Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [Serializable]
    [ExcludeFromCodeCoverage]
    public sealed class BirthCountryIsMandatoryException : DomainException
    {
        /// <inheritdoc/>
        public BirthCountryIsMandatoryException() : this(default(string))
        {
        }

        /// <inheritdoc/>
        public BirthCountryIsMandatoryException(string message) : base(message)
        {
        }

        /// <inheritdoc/>
        public BirthCountryIsMandatoryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <inheritdoc/>
        public BirthCountryIsMandatoryException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        /// <inheritdoc/>
        public BirthCountryIsMandatoryException(string message, IEnumerable<DomainFailure> errors) : base(message, errors)
        {
        }

        /// <inheritdoc/>
        public BirthCountryIsMandatoryException(string message, IEnumerable<DomainFailure> errors, bool appendDefaultMessage) : base(message, errors, appendDefaultMessage)
        {
        }

        /// <inheritdoc/>
        private BirthCountryIsMandatoryException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}