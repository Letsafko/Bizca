namespace Bizca.Core.Infrastructure.Cache
{
    using Bizca.Core.Domain.Cache;
    using Bizca.Core.Domain.Country;
    using System.Threading.Tasks;

    public sealed class CacheCountryRepository : ICountryRepository
    {
        private readonly ICountryRepository decorated;
        private readonly ICacheProvider cache;
        public CacheCountryRepository(ICacheProvider cache, ICountryRepository decorated)
        {
            this.decorated = decorated;
            this.cache = cache;
        }

        public async Task<Country> GetByCodeAsync(string countryCode)
        {
            string cacheKey = GetCacheKey(countryCode);
            return await cache.GetOrCreateAsync(cacheKey, () => decorated.GetByCodeAsync(countryCode));
        }

        public async Task<Country> GetByIdAsync(int countryId)
        {
            string cacheKey = GetCacheKey(countryId);
            return await cache.GetOrCreateAsync(cacheKey, () => decorated.GetByIdAsync(countryId));
        }

        private string GetCacheKey(object value)
        {
            return $"{nameof(Country).ToLower()}_{value}";
        }
    }
}