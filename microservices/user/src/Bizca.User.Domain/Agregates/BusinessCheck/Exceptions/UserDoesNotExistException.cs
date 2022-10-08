namespace Bizca.User.Domain.Agregates.BusinessCheck.Exceptions
{
    using Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;

    [Serializable]
    [ExcludeFromCodeCoverage]
    public sealed class UserDoesNotExistException : ResourceNotFoundException
    {
        public UserDoesNotExistException()
        {
        }

        public UserDoesNotExistException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        public UserDoesNotExistException(string message) : base(message)
        {
        }

        public UserDoesNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public UserDoesNotExistException(string message, string propertyName) : base(message, propertyName)
        {
        }

        public UserDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}