namespace Bizca.Core.Domain.EconomicActivity
{
    using System.Threading.Tasks;

    public interface IEconomicActivityRepository
    {
        Task<EconomicActivity> GetByIdAsync(int economicActivityId);
    }
}