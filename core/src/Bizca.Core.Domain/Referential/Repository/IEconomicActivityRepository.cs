namespace Bizca.Core.Domain.Referential.Repository
{
    using Model;
    using System.Threading.Tasks;

    public interface IEconomicActivityRepository
    {
        Task<EconomicActivity> GetByIdAsync(int economicActivityId);
    }
}