namespace Bizca.Bff.Domain.Entities.User.Exceptions
{
    using Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    public sealed class UserDoesNotExistException : ResourceNotFoundException
    {
        public UserDoesNotExistException(ICollection<DomainFailure> errors) : base(errors)
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