namespace Bizca.Core.Domain
{
    using Bizca.Core.Domain.Exceptions;
    public sealed class RuleResult
    {
        public bool Sucess { get; }
        public DomainFailure Failure { get; }
        public RuleResult(bool sucess, DomainFailure failure)
        {
            Sucess = sucess;
            Failure = failure;
        }
    }
}