namespace Bizca.Bff.Domain.Referentials.Bundle.Exceptions
{
    using Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class BundleDoesNotExistException : ResourceNotFoundException
    {
        public BundleDoesNotExistException()
        {
        }

        public BundleDoesNotExistException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        public BundleDoesNotExistException(string message) : base(message)
        {
        }

        public BundleDoesNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public BundleDoesNotExistException(string message, string propertyName) : base(message, propertyName)
        {
        }

        public BundleDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}