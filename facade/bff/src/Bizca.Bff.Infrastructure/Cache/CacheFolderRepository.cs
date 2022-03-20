namespace Bizca.Bff.Infrastructure.Cache
{
    using Bizca.Bff.Domain.Provider.Folder;
    using Bizca.Core.Domain.Cache;
    using System.Threading.Tasks;

    public sealed class CacheFolderRepository : IFolderRepository
    {
        private readonly IFolderRepository _decorated;
        private readonly ICacheProvider _cacheProvider;
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
