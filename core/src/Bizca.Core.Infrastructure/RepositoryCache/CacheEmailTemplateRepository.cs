namespace Bizca.Core.Infrastructure.RepositoryCache
{
    using Bizca.Core.Domain.Referential.Model;
    using Bizca.Core.Domain.Referential.Repository;
    using Bizca.Core.Infrastructure.Cache;
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