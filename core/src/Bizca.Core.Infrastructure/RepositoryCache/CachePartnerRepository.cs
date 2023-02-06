namespace Bizca.Core.Infrastructure.RepositoryCache
{
    using Domain.Referential.Model;
    using Bizca.Core.Domain.Referential.Repository;
    using Cache;
    using System.Threading.Tasks;

    public sealed class CachePartnerRepository : CacheBase, IPartnerRepository
    {
        private readonly IPartnerRepository _decorated;

        public CachePartnerRepository(ICacheProvider cacheProvider,
            IPartnerRepository decorated)
            : base(cacheProvider)
        {
            _decorated = decorated;
        }

        public async Task<Partner> GetByCodeAsync(string partnerCode)
        {
            string cacheKey = GetCacheKey<Partner>(partnerCode);
            return await CacheProvider.GetOrCreateAsync(cacheKey,
                () => _decorated.GetByCodeAsync(partnerCode));
        }
    }
}