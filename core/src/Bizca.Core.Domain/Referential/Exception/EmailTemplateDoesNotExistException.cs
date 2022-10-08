namespace Bizca.Core.Domain.Referential.Exception
{
    using Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;

    [Serializable]
    [ExcludeFromCodeCoverage]
    public sealed class EmailTemplateDoesNotExistException : ResourceNotFoundException
    {
        public EmailTemplateDoesNotExistException()
        {
        }

        public EmailTemplateDoesNotExistException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        public EmailTemplateDoesNotExistException(string message) : base(message)
        {
        }

        public EmailTemplateDoesNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public EmailTemplateDoesNotExistException(string message, string propertyName) : base(message, propertyName)
        {
        }

        public EmailTemplateDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}