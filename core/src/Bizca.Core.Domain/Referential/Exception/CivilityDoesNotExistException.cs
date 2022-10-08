namespace Bizca.Core.Domain.Referential.Exception
{
    using Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class CivilityDoesNotExistException : ResourceNotFoundException
    {
        public CivilityDoesNotExistException(IEnumerable<DomainFailure> errors)
            : base(errors)
        {
        }

        public CivilityDoesNotExistException(string message) : base(message)
        {
        }

        public CivilityDoesNotExistException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public CivilityDoesNotExistException(string message, string propertyName)
            : base(message, propertyName)
        {
        }

        public CivilityDoesNotExistException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}