namespace Bizca.Core.Infrastructure.Persistence.RepositoryCache
{
    using Cache;
    using Domain.Referential.Model;
    using Domain.Referential.Repository;
    using System.Threading.Tasks;

    public sealed class CacheCivilityRepository : CacheBase, ICivilityRepository
    {
        private readonly ICivilityRepository _decorated;
        public CacheCivilityRepository(ICacheProvider cacheProvider, 
            ICivilityRepository decorated)
            : base(cacheProvider)
        {
            _decorated = decorated;
        }

        public async Task<Civility> GetByIdAsync(int civilityId)
        {
            string cacheKey = GetCacheKey<Civility>(civilityId);
            return await CacheProvider.GetOrCreateAsync(cacheKey, 
                () => _decorated.GetByIdAsync(civilityId));
        }
    }
}