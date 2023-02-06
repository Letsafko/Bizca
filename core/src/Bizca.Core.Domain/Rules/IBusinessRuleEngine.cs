namespace Bizca.Core.Domain.Rules
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBusinessRuleEngine<in TRequest>
    {
        Task CheckRulesAsync(TRequest request);
    }
}