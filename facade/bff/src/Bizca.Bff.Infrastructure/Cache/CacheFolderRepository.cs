namespace Bizca.Bff.Infrastructure.Cache
{
    using Core.Infrastructure.Cache;
    using Domain.Provider.Folder;
    using System.Threading.Tasks;

    public sealed class CacheFolderRepository : IFolderRepository
    {
        private readonly ICacheProvider _cacheProvider;
        private readonly IFolderRepository _decorated;

        public CacheFolderRepository(IFolderRepository decorated, ICacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
            _decorated = decorated;
        }

        public Task<Folder> GetFolderAsync(int partnerId)
        {
            string cacheKey = GetCacheKey(partnerId);
            return _cacheProvider.GetOrCreateAsync(cacheKey, () => _decorated.GetFolderAsync(partnerId));
        }

        private static string GetCacheKey(int partnerId)
        {
            return $"{nameof(Folder).ToLower()}_{partnerId}";
        }
    }
}