namespace Bizca.Core.Domain.Rules.Exception
{
    using Exceptions;
    using System.Collections.Generic;

    public class RuleCheckException: DomainException
    {
        public RuleCheckException(string message, 
            string errorCode = "invalid_rule_check", 
            IEnumerable<DomainFailure> domainFailures = default) : base(message, errorCode, domainFailures)
        {
        }
    }
}