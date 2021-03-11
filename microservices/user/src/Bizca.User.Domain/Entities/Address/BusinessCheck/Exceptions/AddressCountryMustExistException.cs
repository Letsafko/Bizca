namespace Bizca.User.Domain.Entities.Address.BusinessCheck.Exceptions
{
    using Bizca.Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [Serializable]
    [ExcludeFromCodeCoverage]
    public sealed class AddressCountryMustExistException : DomainException
    {
        /// <inheritdoc/>
        public AddressCountryMustExistException() : this(default(string))
        {
        }

        /// <inheritdoc/>
        public AddressCountryMustExistException(string message) : base(message)
        {
        }

        /// <inheritdoc/>
        public AddressCountryMustExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <inheritdoc/>
        public AddressCountryMustExistException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        /// <inheritdoc/>
        public AddressCountryMustExistException(string message, IEnumerable<DomainFailure> errors) : base(message, errors)
        {
        }

        /// <inheritdoc/>
        public AddressCountryMustExistException(string message, IEnumerable<DomainFailure> errors, bool appendDefaultMessage) : base(message, errors, appendDefaultMessage)
        {
        }

        /// <inheritdoc/>
        private AddressCountryMustExistException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}