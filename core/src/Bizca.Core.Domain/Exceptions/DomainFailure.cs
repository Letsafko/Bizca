namespace Bizca.Core.Domain.Exceptions
{
    using System;

    /// <summary>
    ///     defines a business failure
    /// </summary>
    [Serializable]
    public sealed class DomainFailure
    {
        private DomainFailure()
        {
        }

        /// <summary>
        ///     creates a new business failure.
        /// </summary>
        public DomainFailure(string errorMessage, string propertyName = null)
            : this(errorMessage, propertyName, null)
        {
        }

        /// <summary>
        ///     creates a new business failure.
        /// </summary>
        public DomainFailure(string errorMessage, string propertyName, Type exceptionType)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
            ExceptionType = exceptionType;
        }

        /// <summary>
        ///     the property value that caused the failure.
        /// </summary>
        public object AttemptedValue { get; set; }

        /// <summary>
        ///     custom state associated with the failure.
        /// </summary>
        public object CustomState { get; set; }

        /// <summary>
        ///     gets or sets the error code.
        /// </summary>
        public string ErrorCode { get; set; }
        
        /// <summary>
        ///     exception type.
        /// </summary>
        public Type ExceptionType { get; }

        /// <summary>
        ///     the name of the property.
        /// </summary>
        public string PropertyName { get; }

        /// <summary>
        ///     the error message
        /// </summary>
        public string ErrorMessage { get; }
    }
}