namespace Bizca.Core.Infrastructure.Cache
{
    using System;
    using System.Threading.Tasks;

    public interface ICacheProvider
    {
        Task<T> GetOrCreateAsync<T>(string cacheKey,
            Func<Task<T>> createItem,
            TimeSpan? cacheDuration = null) where T : class;

        void Remove(string cacheKey);

        void Reset();
    }
}