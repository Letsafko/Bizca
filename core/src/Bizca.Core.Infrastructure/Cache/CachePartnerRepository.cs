namespace Bizca.Core.Infrastructure.Cache
{
    using Bizca.Core.Domain.Cache;
    using Bizca.Core.Domain.Partner;
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

        public async Task<Partner> GetByCodeAsync(string partnerCode)
        {
            string cacheKey = GetCacheKey(partnerCode);
            return await cache.GetOrCreateAsync(cacheKey, () => decorated.GetByCodeAsync(partnerCode));
        }

        private string GetCacheKey(object value)
        {
            return $"{nameof(Partner).ToLower()}_{value}";
        }
    }
}