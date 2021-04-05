namespace Bizca.Core.Infrastructure.Cache
{
    using Bizca.Core.Domain.Cache;
    using Bizca.Core.Domain.Country;
    using System.Threading;
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

        private static readonly SemaphoreSlim Semaphore = new SemaphoreSlim(1, 1);

        public async Task<Country> GetByCodeAsync(string countryCode)
        {
            string cacheKey = GetCacheKey(countryCode);
            Country cachedReponse = cache.Get<Country>(cacheKey);
            return cachedReponse ?? await cache.GetAsync(cacheKey, Semaphore, () => decorated.GetByCodeAsync(countryCode)).ConfigureAwait(false);
        }

        public async Task<Country> GetByIdAsync(int countryId)
        {
            string cacheKey = GetCacheKey(countryId);
            Country cachedReponse = cache.Get<Country>(cacheKey);
            return cachedReponse ?? await cache.GetAsync(cacheKey, Semaphore, () => decorated.GetByIdAsync(countryId)).ConfigureAwait(false);
        }

        private string GetCacheKey(object value)
        {
            return $"{nameof(Country).ToLower()}_{value}";
        }
    }
}