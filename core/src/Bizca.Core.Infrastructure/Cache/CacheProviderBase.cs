namespace Bizca.Core.Infrastructure.Cache
{
    using System;

    public abstract class CacheProviderBase
    {
        protected abstract T Get<T>(string cacheKey) where T : class;

        protected abstract bool TryAdd<T>(string cacheKey,
            T cacheItem,
            TimeSpan? cacheDuration) where T : class;
    }
}