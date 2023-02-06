namespace Bizca.Core.Domain.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;

    [Serializable]
    public abstract class DomainException : Exception
    {
        public IEnumerable<DomainFailure> Errors { get; }
        public string ErrorCode { get; }

        protected DomainException(string message, 
            string errorCode, 
            IEnumerable<DomainFailure> domainFailures) : base(message)
        {
            Errors = domainFailures ?? new List<DomainFailure>();
            ErrorCode = errorCode;
        }

        public override void GetObjectData([NotNull] SerializationInfo info, 
            StreamingContext context)
        {
            info.AddValue("errors", Errors);
            base.GetObjectData(info, context);
        }
    }
}