﻿namespace Bizca.Core.Domain.Referential.Exception
{
    using Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class EconomicActivityDoesNotExistException : ResourceNotFoundException
    {
        public EconomicActivityDoesNotExistException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        public EconomicActivityDoesNotExistException(string message) : base(message)
        {
        }

        public EconomicActivityDoesNotExistException(string message, Exception innerException) : base(message,
            innerException)
        {
        }

        public EconomicActivityDoesNotExistException(string message, string propertyName) : base(message, propertyName)
        {
        }

        public EconomicActivityDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info,
            context)
        {
        }
    }
}