namespace Bizca.Core.Domain.Partner
{
    using Bizca.Core.Domain.Repositories;
    using System.Threading.Tasks;

    public interface IPartnerRepository : IRepository
    {
        Task<Partner> GetByCodeAsync(string partnerCode);
    }
}
