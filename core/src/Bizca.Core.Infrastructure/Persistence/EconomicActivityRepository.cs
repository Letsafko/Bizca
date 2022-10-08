namespace Bizca.Core.Infrastructure.Persistence
{
    using Domain.Referential.Model;
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
            var result = await GetAsync(new EconomicActivityEntity { Id = economicActivityId });
            return result is null
                    ? default
                    : new EconomicActivity(result.Id, 
                        result.Code, 
                        result.Description);
        }
    }
}