namespace Bizca.Core.Infrastructure.Repository
{
    using Bizca.Core.Domain.Referential.Model;
    using Database;
    using Entity;
    using System.Threading.Tasks;

    public sealed class EconomicActivityRepository : BaseRepository<EconomicActivityEntity>,
        IEconomicActivityRepository
    {
        public EconomicActivityRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<EconomicActivity> GetByIdAsync(int economicActivityId)
        {
            EconomicActivityEntity result = await GetAsync(new EconomicActivityEntity { Id = economicActivityId });
            return result is null
                ? default
                : new EconomicActivity(result.Id,
                    result.Code,
                    result.Description);
        }
    }
}