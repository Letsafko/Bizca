namespace Bizca.User.Domain
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;

    /// <summary>
    ///  domain exception
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class UserDomainException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DomainException" /> class.
        /// </summary>
        public UserDomainException()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DomainException" /> class.
        /// </summary>
        /// <param name="message">Message</param>
        public UserDomainException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DomainException" /> class.
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="innerException">Inner exception</param>
        public UserDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
