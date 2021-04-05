namespace Bizca.Core.Infrastructure.Cache
{
    using Bizca.Core.Domain.Cache;
    using Bizca.Core.Domain.EconomicActivity;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class CacheEconomicActivityRepository : IEconomicActivityRepository
    {
        private readonly IEconomicActivityRepository decorated;
        private readonly ICacheProvider cache;
        public CacheEconomicActivityRepository(ICacheProvider cache, IEconomicActivityRepository decorated)
        {
            this.decorated = decorated;
            this.cache = cache;
        }

        private static readonly SemaphoreSlim Semaphore = new SemaphoreSlim(1, 1);
        public async Task<EconomicActivity> GetByIdAsync(int economicActivityId)
        {
            string cacheKey = GetCacheKey(economicActivityId);
            EconomicActivity cachedReponse = cache.Get<EconomicActivity>(cacheKey);
            return cachedReponse ?? await cache.GetAsync(cacheKey, Semaphore, () => decorated.GetByIdAsync(economicActivityId)).ConfigureAwait(false);
        }

        private string GetCacheKey(object value)
        {
            return $"{nameof(EconomicActivity).ToLower()}_{value}";
        }
    }
}