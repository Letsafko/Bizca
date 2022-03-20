using System.Threading.Tasks;

namespace Bizca.Bff.Domain.Provider.ContactList
{
    public interface IContactListRepository
    {
        Task<ContactList> GetContactListByProcedureAndOrganismAsync(int procedureTypeId, int organismId);
        Task AddNewContactList(ContactList contactList);
    }
}
