namespace Bizca.Core.Domain.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    ///     an exception that represents business error
    /// </summary>
    [Serializable]
    public class DomainException : Exception
    {
        /// <summary>
        ///     Domain failure errors
        /// </summary>
        public IEnumerable<DomainFailure> Errors { get; }

        public DomainException() : this(default(string))
        {
        }

        /// <summary>
        ///     Creates a new DomainException
        /// </summary>
        /// <param name="message"></param>
        public DomainException(string message) : this(message, Enumerable.Empty<DomainFailure>())
        {
        }

        /// <summary>
        ///     Creates a new DomainException
        /// </summary>
        /// <param name="errors"></param>
        public DomainException(IEnumerable<DomainFailure> errors) : base(BuildErrorMessage(errors))
        {
            Errors = errors;
        }

        public DomainException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        ///     Creates a new DomainException
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errors"></param>
        public DomainException(string message, IEnumerable<DomainFailure> errors) : base(message)
        {
            Errors = errors;
        }

        /// <summary>
        ///     Creates a new DomainException
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errors"></param>
        /// <param name="appendDefaultMessage">appends default validation error message to message</param>
        public DomainException(string message, IEnumerable<DomainFailure> errors, bool appendDefaultMessage)
          : base(appendDefaultMessage ? $"{message} {BuildErrorMessage(errors)}" : message)
        {
            Errors = errors;
        }

        private static string BuildErrorMessage(IEnumerable<DomainFailure> errors)
        {
            IEnumerable<string> arr = errors.Select(x => $"{Environment.NewLine} -- {x.PropertyName}: {x.ErrorMessage}");
            return string.Concat(arr);
        }

        protected DomainException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Errors = info.GetValue("errors", typeof(IEnumerable<DomainFailure>)) as IEnumerable<DomainFailure>;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info is null) throw new ArgumentNullException(nameof(info));

            info.AddValue("errors", Errors);
            base.GetObjectData(info, context);
        }
    }
}
