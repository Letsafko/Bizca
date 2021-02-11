namespace Bizca.Core.Domain.Country
{
    using System.Threading.Tasks;

    public interface ICountryRepository
    {
        Task<Country> GetByIdAsync(int countryId);
        Task<Country> GetByCodeAsync(string countryCode);
    }
}