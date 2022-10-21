namespace Bizca.Bff.Infrastructure.Cache
{
    using Core.Infrastructure.Cache;
    using Domain.Referential.Bundle;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public sealed class CacheBundleRepository : IBundleRepository
    {
        private readonly ICacheProvider cache;
        private readonly IBundleRepository decorated;

        public CacheBundleRepository(ICacheProvider cache, IBundleRepository decorated)
        {
            this.decorated = decorated;
            this.cache = cache;
        }

        public async Task<Bundle> GetBundleByIdAsync(int bundleId)
        {
            string cacheKey = GetCacheKey(bundleId);
            return await cache.GetOrCreateAsync(cacheKey, () => decorated.GetBundleByIdAsync(bundleId));
        }

        public async Task<IEnumerable<Bundle>> GetBundlesAsync()
        {
            string cacheKey = GetCacheKey("all");
            return await cache.GetOrCreateAsync(cacheKey, () => decorated.GetBundlesAsync());
        }

        private static string GetCacheKey(object value)
        {
            return $"{nameof(Bundle).ToLower()}_{value}";
        }
    }
}