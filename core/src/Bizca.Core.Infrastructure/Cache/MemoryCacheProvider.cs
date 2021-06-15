namespace Bizca.Core.Infrastructure.Cache
{
    using Bizca.Core.Domain.Cache;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Options;
    using Microsoft.Extensions.Primitives;
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class MemoryCacheProvider : ICacheProvider
    {
        private readonly ConcurrentDictionary<object, SemaphoreSlim> locks;
        private CancellationTokenSource cancellationTokenSource;
        private readonly MemoryCacheOptions cacheOptions;
        private readonly IMemoryCache memoryCache;
        public MemoryCacheProvider(IMemoryCache memoryCache, IOptions<MemoryCacheOptions> cacheOptions = null)
        {
            locks = new ConcurrentDictionary<object, SemaphoreSlim>();
            cancellationTokenSource = new CancellationTokenSource();
            this.cacheOptions = cacheOptions.Value;
            this.memoryCache = memoryCache;
        }

        public async Task<T> GetOrCreateAsync<T>(string cacheKey, Func<Task<T>> createItem, TimeSpan? cacheDuration = null) where T : class
        {
            T cachedReponse = Get<T>(cacheKey);
            if (cachedReponse != null)
                return cachedReponse;

            SemaphoreSlim semaphore = locks.GetOrAdd(cacheKey, k => new SemaphoreSlim(1, 1));
            try
            {
                await semaphore.WaitAsync();
                cachedReponse = Get<T>(cacheKey);
                if (cachedReponse != null)
                    return cachedReponse;

                cachedReponse = await createItem();
                TryAdd(cacheKey, cachedReponse, cacheDuration);
            }
            finally
            {
                semaphore.Release();
            }

            return cachedReponse;
        }
        public void Remove(string cacheKey)
        {
            if (string.IsNullOrWhiteSpace(cacheKey))
                return;

            memoryCache.Remove(cacheKey);
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
              .SetPriority(CacheItemPriority.Normal)
              .AddExpirationToken(new CancellationChangeToken(cancellationTokenSource.Token));
        }
        private bool TryAdd<T>(string cacheKey, T cacheItem, TimeSpan? cacheDuration) where T : class
        {
            if (string.IsNullOrWhiteSpace(cacheKey) || cacheItem is null)
                return false;

            TimeSpan duration = cacheDuration ?? TimeSpan.FromMinutes(cacheOptions.DurationInMinutes);
            MemoryCacheEntryOptions cacheEntryOptions = GetMemoryCacheEntryOptions(duration);
            return !(memoryCache.Set(cacheKey, cacheItem, cacheEntryOptions) is null);
        }
        private T Get<T>(string cacheKey) where T : class
        {
            if (string.IsNullOrWhiteSpace(cacheKey))
                return default;

            object cachedResponse = memoryCache.Get(cacheKey);
            return cachedResponse as T;
        }

        #endregion
    }
}