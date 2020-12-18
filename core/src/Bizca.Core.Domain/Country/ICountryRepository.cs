namespace Bizca.Core.Domain.Country
{
    using System.Threading.Tasks;

    public interface ICountryRepository
    {
        Task<Country> GetByCodeAsync(string countryCode);
    }
}
