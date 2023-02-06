namespace Bizca.Bff.Infrastructure.Cache
{
    using Core.Infrastructure.Cache;
    using Domain.Provider.ContactList;
    using System.Threading.Tasks;

    public sealed class CacheContactListRepository : IContactListRepository
    {
        private readonly ICacheProvider _cacheProvider;
        private readonly IContactListRepository _decorated;

        public CacheContactListRepository(ICacheProvider cacheProvider, IContactListRepository contactListRepository)
        {
            _decorated = contactListRepository;
            _cacheProvider = cacheProvider;
        }

        public Task AddNewContactList(ContactList contactList)
        {
            return _decorated.AddNewContactList(contactList);
        }

        public Task<ContactList> GetContactListByProcedureAndOrganismAsync(int procedureTypeId, int organismId)
        {
            string cacheKey = GetCacheKey(procedureTypeId, organismId);
            return _cacheProvider.GetOrCreateAsync(cacheKey,
                () => _decorated.GetContactListByProcedureAndOrganismAsync(procedureTypeId, organismId));
        }

        private static string GetCacheKey(int procedureTypeId, int organismId)
        {
            return $"{nameof(ContactList).ToLower()}_{procedureTypeId}_{organismId}";
        }
    }
}