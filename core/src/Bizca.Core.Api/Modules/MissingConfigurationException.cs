namespace Bizca.Core.Api.Modules
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    [Serializable]
    public sealed class MissingConfigurationException : Exception
    {
        public string _missingConfiguration { get; }
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
        public MissingConfigurationException(string missingKey, string message, Exception innerException) : base(message, innerException)
        {
            _missingConfiguration = missingKey;
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        private MissingConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            if (info != null)
            {
                _missingConfiguration = info.GetString(nameof(_missingConfiguration));
            }
        }
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info?.AddValue(nameof(_missingConfiguration), _missingConfiguration);

            base.GetObjectData(info, context);
        }
    }
}
