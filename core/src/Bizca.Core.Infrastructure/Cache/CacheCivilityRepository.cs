namespace Bizca.Core.Infrastructure.Cache
{
    using Bizca.Core.Domain.Cache;
    using Bizca.Core.Domain.Civility;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class CacheCivilityRepository : ICivilityRepository
    {
        private readonly ICivilityRepository decorated;
        private readonly ICacheProvider cache;
        public CacheCivilityRepository(ICacheProvider cache, ICivilityRepository decorated)
        {
            this.decorated = decorated;
            this.cache = cache;
        }

        private static readonly SemaphoreSlim Semaphore = new SemaphoreSlim(1, 1);
        public async Task<Civility> GetByIdAsync(int civilityId)
        {
            string cacheKey = GetCacheKey(civilityId);
            Civility cachedReponse = cache.Get<Civility>(cacheKey);
            return cachedReponse ?? await cache.GetAsync(cacheKey, Semaphore, () => decorated.GetByIdAsync(civilityId));
        }

        private string GetCacheKey(object value)
        {
            return $"{nameof(Civility).ToLower()}_{value}";
        }
    }
}