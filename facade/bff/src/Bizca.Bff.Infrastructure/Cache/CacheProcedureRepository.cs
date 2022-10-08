namespace Bizca.Bff.Infrastructure.Cache
{
    using Bizca.Bff.Domain.Referentials.Procedure;
    using Core.Infrastructure.Cache;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public sealed class CacheProcedureRepository : IProcedureRepository
    {
        private readonly IProcedureRepository decorated;
        private readonly ICacheProvider cache;
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