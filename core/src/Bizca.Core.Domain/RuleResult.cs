namespace Bizca.Core.Domain
{
    using Exceptions;

    public sealed class RuleResult
    {
        public RuleResult(bool sucess, DomainFailure failure)
        {
            Sucess = sucess;
            Failure = failure;
        }

        public bool Sucess { get; }
        public DomainFailure Failure { get; }
    }
}