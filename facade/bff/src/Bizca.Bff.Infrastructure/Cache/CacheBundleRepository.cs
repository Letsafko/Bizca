namespace Bizca.Bff.Infrastructure.Cache
{
    using Bizca.Bff.Domain.Referentials.Bundle;
    using Bizca.Core.Domain.Cache;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public sealed class CacheBundleRepository : IBundleRepository
    {
        private readonly IBundleRepository decorated;
        private readonly ICacheProvider cache;
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

        private string GetCacheKey(object value)
        {
            return $"{nameof(Bundle).ToLower()}_{value}";
        }
    }
}