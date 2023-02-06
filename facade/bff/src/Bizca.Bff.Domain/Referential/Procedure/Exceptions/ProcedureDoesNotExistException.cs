namespace Bizca.Bff.Domain.Referential.Procedure.Exceptions
{
    using Bizca.Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class ProcedureDoesNotExistException : ResourceNotFoundException
    {
        public ProcedureDoesNotExistException(ICollection<DomainFailure> errors) : base(errors)
        {
        }

        public ProcedureDoesNotExistException(string message) : base(message)
        {
        }

        public ProcedureDoesNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ProcedureDoesNotExistException(string message, string propertyName) : base(message, propertyName)
        {
        }

        public ProcedureDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}