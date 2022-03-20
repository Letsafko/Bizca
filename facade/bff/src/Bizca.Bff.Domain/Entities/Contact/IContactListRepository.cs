using System.Threading.Tasks;

namespace Bizca.Bff.Domain.Entities.Contact
{
    public interface IContactListRepository
    {
        Task<ContactList> GetContactListByProcedureAndOrganismAsync(int procedureTypeId, int organismId);
        Task AddNewContactList(ContactList contactList);
    }
}
