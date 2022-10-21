namespace Bizca.Core.Infrastructure.RepositoryCache
{
    using Bizca.Core.Domain.Referential.Model;
    using Bizca.Core.Infrastructure.Cache;
    using System.Threading.Tasks;

    public sealed class CacheEconomicActivityRepository : CacheBase, IEconomicActivityRepository
    {
        private readonly IEconomicActivityRepository _decorated;

        public CacheEconomicActivityRepository(ICacheProvider cacheProvider,
            IEconomicActivityRepository decorated)
            : base(cacheProvider)
        {
            _decorated = decorated;
        }

        public async Task<EconomicActivity> GetByIdAsync(int economicActivityId)
        {
            string cacheKey = GetCacheKey<EconomicActivity>(economicActivityId);
            return await CacheProvider.GetOrCreateAsync(cacheKey,
                () => _decorated.GetByIdAsync(economicActivityId));
        }
    }
}