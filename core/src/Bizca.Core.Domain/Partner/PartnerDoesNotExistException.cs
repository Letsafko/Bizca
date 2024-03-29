﻿namespace Bizca.Core.Domain.Partner
{
    using Bizca.Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;

    [Serializable]
    [ExcludeFromCodeCoverage]
    public sealed class PartnerDoesNotExistException : ResourceNotFoundException
    {
        public PartnerDoesNotExistException()
        {
        }

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