namespace Bizca.Core.Domain.Exceptions
{
    using System;

    [Serializable]
    public class ResourceNotFoundException : Exception
    {
        public string ErrorCode { get; }

        protected ResourceNotFoundException(string message, string errorCode = "resource_not_found")
            : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}