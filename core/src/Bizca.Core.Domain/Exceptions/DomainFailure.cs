namespace Bizca.Core.Domain.Exceptions
{
    using System;

    [Serializable]
    public sealed class DomainFailure
    {
        public DomainFailure(string errorMessage, string propertyName = null)
            : this(errorMessage, propertyName, null)
        {
        }

        public DomainFailure(string errorMessage, string propertyName, Type exceptionType)
        {
            ExceptionType = exceptionType;
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
        }

        public object AttemptedValue { get; set; }

        public object CustomState { get; set; }

        public string ErrorCode { get; set; }

        public Type ExceptionType { get; }

        public string PropertyName { get; }

        public string ErrorMessage { get; }
    }
}