namespace Bizca.Bff.Domain.Provider.ContactList
{
    using System.Threading.Tasks;

    public interface IContactListRepository
    {
        Task<ContactList> GetContactListByProcedureAndOrganismAsync(int procedureTypeId, int organismId);
        Task AddNewContactList(ContactList contactList);
    }
}