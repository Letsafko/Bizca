namespace Bizca.Core.Infrastructure.Cache
{
    using Extension;

    public abstract class CacheBase
    {
        protected readonly ICacheProvider CacheProvider;
        protected CacheBase(ICacheProvider cacheProvider)
        {
            CacheProvider = cacheProvider;
        }

        protected static string GetCacheKey<T>(object value) where T : class
        {
            var typeName = typeof(T).GetGenericTypeName().ToLower();
            return $"{typeName}_{value}";
        }
    }
}