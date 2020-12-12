namespace Bizca.Core.Domain.Country
{
    using Bizca.Core.Domain.Repositories;
    using System.Threading.Tasks;

    public interface ICountryRepository : IRepository
    {
        Task<Country> GetByCodeAsync(string countryCode);
    }
}
