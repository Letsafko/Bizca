namespace Bizca.Core.Domain.Country
{
    using Bizca.Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;

    [Serializable]
    [ExcludeFromCodeCoverage]
    public sealed class CountryDoesNotExistException : ResourceNotFoundException
    {
        public CountryDoesNotExistException()
        {
        }

        public CountryDoesNotExistException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        public CountryDoesNotExistException(string message) : base(message)
        {
        }

        public CountryDoesNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public CountryDoesNotExistException(string message, string propertyName) : base(message, propertyName)
        {
        }

        public CountryDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}