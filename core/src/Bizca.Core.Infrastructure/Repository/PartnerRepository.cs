namespace Bizca.Core.Infrastructure.Repository
{
    using Bizca.Core.Domain.Referential.Model;
    using Bizca.Core.Domain.Referential.Repository;
    using Database;
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