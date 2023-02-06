namespace Bizca.Core.Domain.Referential.Repository
{
    using Model;
    using System.Threading.Tasks;

    public interface ICountryRepository
    {
        Task<Country> GetByCodeAsync(string countryCode);
        Task<Country> GetByIdAsync(int countryId);
    }
}