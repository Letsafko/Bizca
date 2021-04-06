namespace Bizca.Core.Infrastructure.Cache
{
    using Bizca.Core.Domain.Cache;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Options;
    using Microsoft.Extensions.Primitives;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class MemoryCacheProvider : ICacheProvider
    {
        private CancellationTokenSource cancellationTokenSource;
        private readonly MemoryCacheOptions cacheOptions;
        private readonly IMemoryCache memoryCache;
        public MemoryCacheProvider(IMemoryCache memoryCache, IOptions<MemoryCacheOptions> cacheOptions = null)
        {
            cancellationTokenSource = new CancellationTokenSource();
            this.cacheOptions = cacheOptions.Value;
            this.memoryCache = memoryCache;
        }

        public async Task<T> GetAsync<T>(string cacheKey, SemaphoreSlim semaphore, Func<Task<T>> func) where T : class
        {
            T cachedReponse = Get<T>(cacheKey);
            if (cachedReponse != null)
                return cachedReponse;

            try
            {
                await semaphore.WaitAsync().ConfigureAwait(false);
                cachedReponse = Get<T>(cacheKey);
                if (cachedReponse != null)
                    return cachedReponse;

                cachedReponse = await func().ConfigureAwait(false);
                TryAdd(cacheKey, cachedReponse);
            }
            finally
            {
                semaphore.Release();
            }

            return cachedReponse;
        }

        public bool TryAdd<T>(string cacheKey, T cacheItem, TimeSpan cacheDuration) where T : class
        {
            if (string.IsNullOrWhiteSpace(cacheKey) || cacheItem is null)
                return false;

            MemoryCacheEntryOptions cacheEntryOptions = GetMemoryCacheEntryOptions(cacheDuration);
            return !(memoryCache.Set(cacheKey, cacheItem, cacheEntryOptions) is null);
        }

        public bool TryAdd<T>(string cacheKey, T cacheItem) where T : class
        {
            return TryAdd(cacheKey, cacheItem, TimeSpan.FromMinutes(cacheOptions.DurationInMinutes));
        }

        public void Clear(string cacheKey)
        {
            if (string.IsNullOrWhiteSpace(cacheKey))
                return;

            memoryCache.Remove(cacheKey);
        }

        public T Get<T>(string cacheKey) where T : class
        {
            if (string.IsNullOrWhiteSpace(cacheKey))
                return default;

            object cachedResponse = memoryCache.Get(cacheKey);
            return cachedResponse as T;
        }

        public void Reset()
        {
            if (cancellationTokenSource?.IsCancellationRequested == false &&
                cancellationTokenSource.Token.CanBeCanceled)
            {
                cancellationTokenSource.Cancel();
                cancellationTokenSource.Dispose();
            }

            cancellationTokenSource = new CancellationTokenSource();
        }

        #region private helpers

        private MemoryCacheEntryOptions GetMemoryCacheEntryOptions(TimeSpan cacheDuration)
        {
            return new MemoryCacheEntryOptions()
              .SetAbsoluteExpiration(cacheDuration)
              .SetSlidingExpiration(cacheDuration)
              .SetPriority(CacheItemPriority.Normal)
              .AddExpirationToken(new CancellationChangeToken(cancellationTokenSource.Token));
        }

        #endregion
    }
}