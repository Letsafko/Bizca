namespace Bizca.Core.Infrastructure.Cache
{
    using Bizca.Core.Domain.Cache;
    using Bizca.Core.Domain.EconomicActivity;
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

        public async Task<EconomicActivity> GetByIdAsync(int economicActivityId)
        {
            string cacheKey = GetCacheKey(economicActivityId);
            return await cache.GetOrCreateAsync(cacheKey, () => decorated.GetByIdAsync(economicActivityId));
        }

        private string GetCacheKey(object value)
        {
            return $"{nameof(EconomicActivity).ToLower()}_{value}";
        }
    }
}