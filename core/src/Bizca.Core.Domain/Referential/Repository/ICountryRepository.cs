namespace Bizca.Core.Domain.Referential.Repository
{
    using Model;
    using System.Threading.Tasks;

    public interface ICountryRepository
    {
        Task<Country> GetByIdAsync(int countryId);
        Task<Country> GetByCodeAsync(string countryCode);
    }
}