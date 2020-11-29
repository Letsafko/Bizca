namespace Bizca.Core.Domain.Rules
{
    using System.Threading.Tasks;

    public interface IBusinessRule<TRequest>
    {
        Task<RuleResult> CheckAsync(TRequest request);
    }
}
