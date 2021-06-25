namespace Bizca.User.Domain.Agregates.BusinessCheck.Exceptions
{
    using Bizca.Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;

    [Serializable]
    [ExcludeFromCodeCoverage]
    public sealed class UserAlreadyExistException : DomainException
    {
        public UserAlreadyExistException()
        {
        }

        public UserAlreadyExistException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        public UserAlreadyExistException(string message) : base(message)
        {
        }

        public UserAlreadyExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public UserAlreadyExistException(string message, string propertyName) : base(message, propertyName)
        {
        }

        public UserAlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}