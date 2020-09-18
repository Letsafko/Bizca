namespace Bizca.User.Domain
{
    using System.Threading.Tasks;
    public interface IBusinessRuleEngine<in TRequest>
    {
        Task CheckRulesAsync(TRequest request);
    }
}
