
namespace Bizca.Bff.Domain.Entities.User.Exceptions
{
    using Bizca.Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    public sealed class FolderDoesNotExistException : ResourceNotFoundException
    {
        public FolderDoesNotExistException()
        {
        }

        public FolderDoesNotExistException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        public FolderDoesNotExistException(string message) : base(message)
        {
        }

        public FolderDoesNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public FolderDoesNotExistException(string message, string propertyName) : base(message, propertyName)
        {
        }

        public FolderDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}