namespace Bizca.Core.Domain.Exceptions
{
    using System;
    using System.Collections.Generic;

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
        public DomainFailure(string propertyName, string errorMessage) : this(propertyName, errorMessage, null)
        {
        }

        /// <summary>
        ///     creates a new business failure.
        /// </summary>
        public DomainFailure(string propertyName, string errorMessage, object attemptedValue)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
            AttemptedValue = attemptedValue;
        }

        /// <summary>
        ///     the name of the property.
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        ///     the error message
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        ///     the property value that caused the failure.
        /// </summary>
        public object AttemptedValue { get; set; }

        /// <summary>
        ///     custom state associated with the failure.
        /// </summary>
        public object CustomState { get; set; }

        /// <summary>
        ///     custom severity level associated with the failure.
        /// </summary>
        public Severity Severity { get; set; } = Severity.Error;

        /// <summary>
        ///     gets or sets the error code.
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        ///     gets or sets the formatted message placeholder values.
        /// </summary>
        public Dictionary<string, object> FormattedMessagePlaceholderValues { get; set; }

        /// <summary>
        /// Creates a textual representation of the failure.
        /// </summary>
        public override string ToString()
        {
            return ErrorMessage;
        }
    }
}
