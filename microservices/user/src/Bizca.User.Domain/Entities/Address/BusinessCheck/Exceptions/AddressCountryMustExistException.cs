namespace Bizca.User.Domain.Entities.Address.BusinessCheck.Exceptions
{
    using Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;

    [Serializable]
    [ExcludeFromCodeCoverage]
    public sealed class AddressCountryMustExistException : DomainException
    {
        public AddressCountryMustExistException()
        {
        }

        public AddressCountryMustExistException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        public AddressCountryMustExistException(string message) : base(message)
        {
        }

        public AddressCountryMustExistException(string message, Exception innerException) : base(message,
            innerException)
        {
        }

        public AddressCountryMustExistException(string message, string propertyName) : base(message, propertyName)
        {
        }

        public AddressCountryMustExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}