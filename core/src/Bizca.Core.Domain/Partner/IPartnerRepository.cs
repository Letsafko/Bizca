namespace Bizca.Core.Domain.Partner
{
    using System.Threading.Tasks;

    public interface IPartnerRepository
    {
        Task<Partner> GetByCodeAsync(string partnerCode);
    }
}
