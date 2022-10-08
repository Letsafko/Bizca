namespace Bizca.Core.Infrastructure.Cache
{
    public abstract class CacheBase
    {
        protected readonly ICacheProvider CacheProvider;

        protected CacheBase(ICacheProvider cacheProvider)
        {
            CacheProvider = cacheProvider;
        }

        protected static string GetCacheKey<T>(object value) where T : class
        {
            return $"{typeof(T).Name.ToLower()}_{value}";
        }
    }
}