namespace Bizca.Core.Domain.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    ///     A business exception that represents resource not found.
    /// </summary>
    [Serializable]
    public class ResourceNotFoundException : Exception
    {
        /// <summary>
        ///     Domain failure errors
        /// </summary>
        public IEnumerable<DomainFailure> Errors { get; }

        /// <inheritdoc/>
        public ResourceNotFoundException() : base()
        {
        }

        /// <inheritdoc/>
        public ResourceNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="ResourceNotFoundException"/>
        /// </summary>
        /// <param name="errors"></param>
        public ResourceNotFoundException(IEnumerable<DomainFailure> errors) : base(BuildErrorMessage(errors))
        {
            Errors = errors;
        }

        /// <summary>
        ///     Creates a new instance of <see cref="ResourceNotFoundException"/> 
        /// </summary>
        /// <param name="message"></param>
        public ResourceNotFoundException(string message) : this(new List<DomainFailure> { new DomainFailure(message) })
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="ResourceNotFoundException"/> 
        /// </summary>
        /// <param name="message"></param>
        public ResourceNotFoundException(string message, string propertyName) : this(new List<DomainFailure> { new DomainFailure(message, propertyName) })
        {
        }

        protected ResourceNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Errors = info.GetValue("errors", typeof(IEnumerable<DomainFailure>)) as IEnumerable<DomainFailure>;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info is null) throw new ArgumentNullException(nameof(info));

            info.AddValue("errors", Errors);
            base.GetObjectData(info, context);
        }

        private static string BuildErrorMessage(IEnumerable<DomainFailure> errors)
        {
            IEnumerable<string> arr = errors.Select(x => $"{Environment.NewLine} -- {x.PropertyName}: {x.ErrorMessage}");
            return string.Concat(arr);
        }
    }
}