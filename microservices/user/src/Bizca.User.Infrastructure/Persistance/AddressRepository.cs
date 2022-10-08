namespace Bizca.User.Infrastructure.Persistance
{
    using Core.Infrastructure.Database;
    using Domain.Entities.Address;
    using Domain.Entities.Address.Repositories;
    using Extensions;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;

    public sealed class AddressRepository : IAddressRepository
    {
        private const string upSertAddressesStoredProcedure = "[usr].[usp_upsert_address]";
        private readonly IUnitOfWork unitOfWork;

        public AddressRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> UpsertAsync(int userId, IEnumerable<Address> addresses)
        {
            var parameters = new { addresses = new TableValueParameter(addresses.ToDataTable(userId)) };

            return await unitOfWork.Connection
                .ExecuteAsync(upSertAddressesStoredProcedure,
                    parameters,
                    unitOfWork.Transaction,
                    commandType: CommandType.StoredProcedure)
                .ConfigureAwait(false) > 0;
        }
    }
}