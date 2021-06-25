namespace Bizca.Core.Domain.Civility
{
    using Bizca.Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;

    [Serializable]
    [ExcludeFromCodeCoverage]
    public sealed class CivilityDoesNotExistException : ResourceNotFoundException
    {
        public CivilityDoesNotExistException()
        {
        }

        public CivilityDoesNotExistException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        public CivilityDoesNotExistException(string message) : base(message)
        {
        }

        public CivilityDoesNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public CivilityDoesNotExistException(string message, string propertyName) : base(message, propertyName)
        {
        }

        public CivilityDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}