namespace Bizca.Core.Infrastructure.Cache
{
    using Bizca.Core.Domain.Cache;
    using Bizca.Core.Domain.EmailTemplate;
    using System.Threading.Tasks;

    public sealed class CacheEmailTemplateRepository : IEmailTemplateRepository
    {
        private readonly IEmailTemplateRepository decorated;
        private readonly ICacheProvider cache;
        public CacheEmailTemplateRepository(ICacheProvider cache, IEmailTemplateRepository decorated)
        {
            this.decorated = decorated;
            this.cache = cache;
        }

        public async Task<EmailTemplate> GetByIdAsync(int emailTemplateTypeId, string languageCode = "fr")
        {
            string cacheKey = GetCacheKey(emailTemplateTypeId);
            return await cache.GetOrCreateAsync(cacheKey, () => decorated.GetByIdAsync(emailTemplateTypeId, languageCode));
        }

        private string GetCacheKey(object value)
        {
            return $"{nameof(EmailTemplate).ToLower()}_{value}";
        }
    }
}
