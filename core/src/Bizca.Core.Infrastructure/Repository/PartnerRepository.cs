namespace Bizca.Core.Infrastructure.Repository
{
    using Domain.Referential.Model;
    using Bizca.Core.Domain.Referential.Repository;
    using Database;
    using Entity;
    using Newtonsoft.Json;
    using System.Threading.Tasks;

    public sealed class PartnerRepository : BaseRepository<PartnerEntity>, IPartnerRepository
    {
        public PartnerRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<Partner> GetByCodeAsync(string partnerCode)
        {
            var partnerEntity = await GetAsync(new PartnerEntity { Code = partnerCode });
            if (partnerEntity is null)
                return null;
        
            var partnerConfiguration = JsonConvert.DeserializeObject<PartnerConfiguration>(partnerEntity.Configuration);
            return new Partner(partnerEntity.Id,
                partnerEntity.Code,
                partnerEntity.Description,
                partnerConfiguration);
        }
    }
}