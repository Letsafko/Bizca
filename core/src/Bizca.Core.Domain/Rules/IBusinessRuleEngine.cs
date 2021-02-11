namespace Bizca.Core.Domain.Rules
{
    using System.Threading.Tasks;
    public interface IBusinessRuleEngine<in TRequest>
    {
        Task<RuleResultCollection> CheckRulesAsync(TRequest request);
    }
}