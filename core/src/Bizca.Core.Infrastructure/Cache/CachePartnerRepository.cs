namespace Bizca.Core.Infrastructure.Cache
{
    using Bizca.Core.Domain.Cache;
    using Bizca.Core.Domain.Partner;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class CachePartnerRepository : IPartnerRepository
    {
        private readonly IPartnerRepository decorated;
        private readonly ICacheProvider cache;
        public CachePartnerRepository(ICacheProvider cache, IPartnerRepository decorated)
        {
            this.decorated = decorated;
            this.cache = cache;
        }

        private static readonly SemaphoreSlim Semaphore = new SemaphoreSlim(1, 1);
        public async Task<Partner> GetByCodeAsync(string partnerCode)
        {
            string cacheKey = GetCacheKey(partnerCode);
            Partner cachedReponse = cache.Get<Partner>(cacheKey);
            return cachedReponse ?? await cache.GetAsync(cacheKey, Semaphore, () => decorated.GetByCodeAsync(partnerCode)).ConfigureAwait(false);
        }

        private string GetCacheKey(object value)
        {
            return $"{nameof(Partner).ToLower()}_{value}";
        }
    }
}