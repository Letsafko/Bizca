namespace Bizca.Core.Infrastructure.Persistence
{
    using Domain.Referential.Model;
    using Domain.Referential.Repository;
    using Entity;
    using System.Threading.Tasks;

    public sealed class PartnerRepository : BaseRepository<PartnerEntity>, IPartnerRepository
    {
        public PartnerRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<Partner> GetByCodeAsync(string partnerCode)
        {
            PartnerEntity result = await GetAsync(new PartnerEntity { Code = partnerCode });

            return result is null
                ? default
                : new Partner(result.Id,
                    result.Code,
                    result.Description);
        }
    }
}