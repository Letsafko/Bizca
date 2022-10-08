namespace Bizca.Core.Api
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    [Serializable]
    public sealed class MissingConfigurationException : Exception
    {
        public MissingConfigurationException()
        {
        }

        public MissingConfigurationException(string missingKey) : this(missingKey, default(string))
        {
        }

        public MissingConfigurationException(string missingKey, string message) : this(missingKey, message, null)
        {
        }

        public MissingConfigurationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public MissingConfigurationException(string missingKey, string message, Exception innerException) : base(
            message, innerException)
        {
            missingConfiguration = missingKey;
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        private MissingConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            if (info != null) missingConfiguration = info.GetString(nameof(missingConfiguration));
        }

        public string missingConfiguration { get; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info?.AddValue(nameof(missingConfiguration), missingConfiguration);

            base.GetObjectData(info, context);
        }
    }
}