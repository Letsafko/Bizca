namespace Bizca.Core.Infrastructure.RepositoryCache
{
    using Bizca.Core.Domain.Referential.Model;
    using Bizca.Core.Domain.Referential.Repository;
    using Bizca.Core.Infrastructure.Cache;
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
            var cacheKey = GetCacheKey<Civility>(civilityId);
            return await CacheProvider.GetOrCreateAsync(cacheKey,
                () => _decorated.GetByIdAsync(civilityId));
        }
    }
}