namespace Bizca.Core.Domain.Referential.Model
{
    using System.Threading.Tasks;

    public interface IEconomicActivityRepository
    {
        Task<EconomicActivity> GetByIdAsync(int economicActivityId);
    }
}