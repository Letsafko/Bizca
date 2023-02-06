namespace Bizca.Core.Infrastructure.Repository
{
    using Bizca.Core.Domain.Referential.Model;
    using Bizca.Core.Domain.Referential.Repository;
    using Database;
    using Entity;
    using System.Threading.Tasks;

    public sealed class CountryRepository : BaseRepository<CountryEntity>, ICountryRepository
    {
        public CountryRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<Country> GetByCodeAsync(string countryCode)
        {
            CountryEntity result = await GetAsync(new CountryEntity { Code = countryCode });

            return GetCountry(result);
        }

        public async Task<Country> GetByIdAsync(int countryId)
        {
            CountryEntity result = await GetAsync(new CountryEntity { Id = countryId });

            return GetCountry(result);
        }

        private static Country GetCountry(CountryEntity result)
        {
            return result is null
                ? default
                : new Country(result.Id,
                    result.Code,
                    result.Description);
        }
    }
}