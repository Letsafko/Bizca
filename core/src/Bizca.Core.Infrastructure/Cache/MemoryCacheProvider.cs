namespace Bizca.Core.Infrastructure.Cache
{
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Options;
    using Microsoft.Extensions.Primitives;
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class MemoryCacheProvider : CacheProviderBase, ICacheProvider
    {
        private readonly MemoryCacheOptions _cacheOptions;
        private readonly ConcurrentDictionary<object, SemaphoreSlim> _locks;
        private readonly IMemoryCache _memoryCache;
        private CancellationTokenSource _cancellationTokenSource;

        public MemoryCacheProvider(IMemoryCache memoryCache,
            IOptions<MemoryCacheOptions> cacheOptions)
        {
            _locks = new ConcurrentDictionary<object, SemaphoreSlim>();
            _cancellationTokenSource = new CancellationTokenSource();
            _cacheOptions = cacheOptions!.Value;
            _memoryCache = memoryCache;
        }

        public async Task<T> GetOrCreateAsync<T>(string cacheKey,
            Func<Task<T>> createItem,
            TimeSpan? cacheDuration = null) where T : class
        {
            var cachedItem = Get<T>(cacheKey);
            if (cachedItem != null)
                return cachedItem;

            SemaphoreSlim semaphore =
                _locks.GetOrAdd(cacheKey, _ => new SemaphoreSlim(1, 1));

            try
            {
                await semaphore.WaitAsync();
                cachedItem = Get<T>(cacheKey);
                if (cachedItem != null)
                    return cachedItem;

                cachedItem = await createItem();
                TryAdd(cacheKey, cachedItem, cacheDuration);
            }
            finally
            {
                semaphore.Release();
            }

            return cachedItem;
        }

        public void Remove(string cacheKey)
        {
            if (string.IsNullOrWhiteSpace(cacheKey))
                return;

            _memoryCache.Remove(cacheKey);
        }

        public void Reset()
        {
            if (!_cancellationTokenSource.IsCancellationRequested &&
                _cancellationTokenSource.Token.CanBeCanceled)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
            }

            _cancellationTokenSource = new CancellationTokenSource();
        }

        protected override bool TryAdd<T>(string cacheKey,
            T cacheItem,
            TimeSpan? cacheDuration) where T : class
        {
            if (string.IsNullOrWhiteSpace(cacheKey) || cacheItem is null)
                return false;

            TimeSpan duration = cacheDuration ?? TimeSpan.FromMinutes(_cacheOptions.DurationInMinutes);
            MemoryCacheEntryOptions cacheEntryOptions =
                GetMemoryCacheEntryOptions(duration, _cancellationTokenSource.Token);

            return !(_memoryCache.Set(cacheKey, cacheItem, cacheEntryOptions) is null);
        }

        protected override T Get<T>(string cacheKey) where T : class
        {
            if (string.IsNullOrWhiteSpace(cacheKey))
                return default;

            object cachedItem = _memoryCache.Get(cacheKey);
            return cachedItem as T;
        }

        private static MemoryCacheEntryOptions GetMemoryCacheEntryOptions(TimeSpan cacheDuration,
            CancellationToken cancellationToken,
            CacheItemPriority cacheItemPriority = CacheItemPriority.Normal)
        {
            return new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(cacheDuration)
                .SetPriority(cacheItemPriority)
                .AddExpirationToken(new CancellationChangeToken(cancellationToken));
        }
    }
}