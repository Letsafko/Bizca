namespace Bizca.Core.Infrastructure.Persistence.RepositoryCache
{
    using Cache;
    using Domain.Referential.Model;
    using Domain.Referential.Repository;
    using System.Threading.Tasks;

    public sealed class CacheEmailTemplateRepository : CacheBase, IEmailTemplateRepository
    {
        private readonly IEmailTemplateRepository _decorated;
        public CacheEmailTemplateRepository(ICacheProvider cacheProvider, 
            IEmailTemplateRepository decorated)
            : base(cacheProvider)
        {
            _decorated = decorated;
        }

        public async Task<EmailTemplate> GetByIdAsync(int emailTemplateTypeId, 
            string languageCode = "fr")
        {
            string cacheKey = GetCacheKey<EmailTemplate>(emailTemplateTypeId);
            return await CacheProvider.GetOrCreateAsync(cacheKey, 
                () => _decorated.GetByIdAsync(emailTemplateTypeId, languageCode));
        }
    }
}
