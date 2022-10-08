namespace Bizca.Bff.Infrastructure.Cache
{
    using Core.Infrastructure.Cache;
    using Domain.Referentials.Procedure;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public sealed class CacheProcedureRepository : IProcedureRepository
    {
        private readonly ICacheProvider cache;
        private readonly IProcedureRepository decorated;

        public CacheProcedureRepository(ICacheProvider cache, IProcedureRepository decorated)
        {
            this.decorated = decorated;
            this.cache = cache;
        }

        public async Task<Procedure> GetProcedureByTypeIdAndCodeInseeAsync(int procedureTypeId, string codeInsee)
        {
            string cacheKey = GetCacheKey($"{codeInsee}_{procedureTypeId}");
            return await cache.GetOrCreateAsync
            (
                cacheKey,
                () => decorated.GetProcedureByTypeIdAndCodeInseeAsync(procedureTypeId, codeInsee)
            );
        }

        public async Task<IEnumerable<Procedure>> GetProceduresAsync()
        {
            string cacheKey = GetCacheKey("all");
            return await cache.GetOrCreateAsync(cacheKey, () => decorated.GetProceduresAsync());
        }

        public Task<IEnumerable<Procedure>> GetProceduresByActiveSubscriptionsAsync()
        {
            return decorated.GetProceduresByActiveSubscriptionsAsync();
        }

        private static string GetCacheKey(object value)
        {
            return $"{nameof(Procedure).ToLower()}_{value}";
        }
    }
}