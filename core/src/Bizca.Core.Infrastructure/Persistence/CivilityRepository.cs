namespace Bizca.Core.Infrastructure.Persistence
{
    using Domain.Referential.Model;
    using Domain.Referential.Repository;
    using Entity;
    using System.Threading.Tasks;

    public sealed class CivilityRepository : BaseRepository<CivilityEntity>, ICivilityRepository
    {
        public CivilityRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<Civility> GetByIdAsync(int civilityId)
        {
            CivilityEntity result = await GetAsync(new CivilityEntity { Id = civilityId });
            return result is null
                ? default
                : new Civility(result.Id, result.Code);
        }
    }
}