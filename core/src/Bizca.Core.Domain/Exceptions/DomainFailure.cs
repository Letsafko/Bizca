namespace Bizca.Core.Domain.Exceptions
{
    using System;

    [Serializable]
    public sealed class DomainFailure
    {
        public DomainFailure(string errorMessage, string propertyName, Type exceptionType = null)
        {
            ExceptionType = exceptionType;
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
        }
        public string PropertyName { get; }
        public string ErrorMessage { get; }
        public Type ExceptionType { get; }
    }
}