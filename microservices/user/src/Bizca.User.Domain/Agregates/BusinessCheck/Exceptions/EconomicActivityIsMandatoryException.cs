namespace Bizca.User.Domain.Agregates.BusinessCheck.Exceptions
{
    using Core.Domain.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;

    [Serializable]
    [ExcludeFromCodeCoverage]
    public sealed class EconomicActivityIsMandatoryException : DomainException
    {
        public EconomicActivityIsMandatoryException()
        {
        }

        public EconomicActivityIsMandatoryException(IEnumerable<DomainFailure> errors) : base(errors)
        {
        }

        public EconomicActivityIsMandatoryException(string message) : base(message)
        {
        }

        public EconomicActivityIsMandatoryException(string message, Exception innerException) : base(message,
            innerException)
        {
        }

        public EconomicActivityIsMandatoryException(string message, string propertyName) : base(message, propertyName)
        {
        }

        public EconomicActivityIsMandatoryException(SerializationInfo info, StreamingContext context) : base(info,
            context)
        {
        }
    }
}