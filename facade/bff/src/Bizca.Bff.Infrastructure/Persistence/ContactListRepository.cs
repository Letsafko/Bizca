namespace Bizca.Bff.Infrastructure.Persistence
{
    using Domain.Provider.ContactList;
    using Bizca.Core.Infrastructure.Database;
    using Bizca.Core.Infrastructure.Repository;
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
            var queryParams = new
            {
                procedureTypeId,
                organismId
            };
            
            var result = await FindAsync(statement =>
            {
                statement
                    .AttachToTransaction(UnitOfWork.Transaction)
                    .WithParameters(queryParams)
                    .Where(@$"{nameof(ContactList.ProcedureTypeId):C} = {nameof(queryParams.procedureTypeId):P} AND
                              {nameof(ContactList.OrganismId):C} = {nameof(queryParams.organismId):P}");
            });

            return result.FirstOrDefault();
        }
    }
}