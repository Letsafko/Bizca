namespace Bizca.User.Domain.Agregates.BusinessCheck.Exceptions
{
    using Bizca.Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [Serializable]
    [ExcludeFromCodeCoverage]
    public sealed class BirthCityIsMandatoryException : DomainException
    {
        /// <inheritdoc/>
        public BirthCityIsMandatoryException() : this(default(string))
        {
        }

        /// <inheritdoc/>
        public BirthCityIsMandatoryException(string message) : base(message)
        {
        }

        /// <inheritdoc/>
        public BirthCityIsMandatoryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <inheritdoc/>
        public BirthCityIsMandatoryException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        /// <inheritdoc/>
        public BirthCityIsMandatoryException(string message, IEnumerable<DomainFailure> errors) : base(message, errors)
        {
        }

        /// <inheritdoc/>
        public BirthCityIsMandatoryException(string message, IEnumerable<DomainFailure> errors, bool appendDefaultMessage) : base(message, errors, appendDefaultMessage)
        {
        }

        /// <inheritdoc/>
        private BirthCityIsMandatoryException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}