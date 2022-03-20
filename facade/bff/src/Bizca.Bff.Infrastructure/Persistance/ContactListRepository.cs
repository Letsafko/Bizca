namespace Bizca.Bff.Infrastructure.Persistance
{
    using Bizca.Bff.Domain.Provider.ContactList;
    using Bizca.Core.Domain;
    using Bizca.Core.Infrastructure;
    using System.Linq;
    using System.Threading.Tasks;

    public sealed class ContactListRepository : BaseRepository<ContactList>, IContactListRepository
    {
        public ContactListRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public Task AddNewContactList(ContactList contactList)
        {
            return InsertAsync(contactList);
        }

        public async Task<ContactList> GetContactListByProcedureAndOrganismAsync(int procedureTypeId, int organismId)
        {
            var procedureTypeIdColumn = GetColumnAttributeName(nameof(ContactList.ProcedureTypeId));
            var organismIdColumn = GetColumnAttributeName(nameof(ContactList.OrganismId));
            var result = await FindAsync(statement =>
            {
                if (UnitOfWork.Transaction != null)
                {
                    statement.AttachToTransaction(UnitOfWork.Transaction);
                }

                statement
                    .Where($"{procedureTypeIdColumn} = @procedureTypeId AND {organismIdColumn} = @organismId")
                    .WithParameters(new { procedureTypeId, organismId });
            });

            return result?.FirstOrDefault();
        }
    }
}
