namespace Bizca.Core.Infrastructure.Persistence.RepositoryCache
{
    using Cache;
    using Domain.Referential.Model;
    using Domain.Referential.Repository;
    using System.Threading.Tasks;

    public sealed class CacheCountryRepository : CacheBase, ICountryRepository
    {
        private readonly ICountryRepository _decorated;
        public CacheCountryRepository(ICacheProvider cacheProvider, 
            ICountryRepository decorated)
            : base(cacheProvider)
        {
            _decorated = decorated;
        }

        public async Task<Country> GetByCodeAsync(string countryCode)
        {
            string cacheKey = GetCacheKey<Country>(countryCode);
            return await CacheProvider.GetOrCreateAsync(cacheKey, 
                () => _decorated.GetByCodeAsync(countryCode));
        }

        public async Task<Country> GetByIdAsync(int countryId)
        {
            string cacheKey = GetCacheKey<Country>(countryId);
            return await CacheProvider.GetOrCreateAsync(cacheKey, 
                () => _decorated.GetByIdAsync(countryId));
        }
    }
}