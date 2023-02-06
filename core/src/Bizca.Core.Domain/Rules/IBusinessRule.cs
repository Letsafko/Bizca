namespace Bizca.Core.Domain.Rules
{
    using System.Threading.Tasks;

    public interface IBusinessRule<in TRequest>
    {
        Task<CheckResult> CheckAsync(TRequest request);
    }
}