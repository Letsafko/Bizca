namespace Bizca.Core.Domain.Referential.Repository
{
    using Model;
    using System.Threading.Tasks;

    public interface IPartnerRepository
    {
        Task<Partner> GetByCodeAsync(string partnerCode);
    }
}