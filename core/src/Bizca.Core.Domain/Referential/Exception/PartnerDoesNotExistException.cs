namespace Bizca.Core.Domain.Referential.Exception
{
    using Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class PartnerDoesNotExistException : ResourceNotFoundException
    {
        public PartnerDoesNotExistException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        public PartnerDoesNotExistException(string message) : base(message)
        {
        }

        public PartnerDoesNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public PartnerDoesNotExistException(string message, string propertyName) : base(message, propertyName)
        {
        }

        public PartnerDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}