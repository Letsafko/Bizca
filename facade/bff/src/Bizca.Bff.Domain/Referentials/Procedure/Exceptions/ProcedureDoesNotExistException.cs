namespace Bizca.Bff.Domain.Referentials.Procedure.Exceptions
{
    using Bizca.Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;

    [Serializable]
    public sealed class ProcedureDoesNotExistException : NotFoundDomainException
    {
        /// <inheritdoc/>
        public ProcedureDoesNotExistException() : this(default(string))
        {
        }

        /// <inheritdoc/>
        public ProcedureDoesNotExistException(string message) : base(message)
        {
        }

        /// <inheritdoc/>
        public ProcedureDoesNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <inheritdoc/>
        public ProcedureDoesNotExistException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        /// <inheritdoc/>
        public ProcedureDoesNotExistException(string message, IEnumerable<DomainFailure> errors) : base(message, errors)
        {
        }

        /// <inheritdoc/>
        public ProcedureDoesNotExistException(string message, IEnumerable<DomainFailure> errors, bool appendDefaultMessage) : base(message, errors, appendDefaultMessage)
        {
        }

        /// <inheritdoc/>
        private ProcedureDoesNotExistException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}
