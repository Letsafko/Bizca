namespace Bizca.Core.Domain.Cache
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public interface ICacheProvider
    {
        Task<T> GetAsync<T>(string cacheKey, SemaphoreSlim semaphore, Func<Task<T>> func) where T : class;
        bool TryAdd<T>(string cacheKey, T cacheItem, TimeSpan cacheDuration) where T : class;
        bool TryAdd<T>(string cacheKey, T cacheItem) where T : class;
        T Get<T>(string cacheKey) where T : class;
        void Clear(string cacheKey);
    }
}