namespace Bizca.User.Infrastructure.Persistence
{
    using Bizca.Core.Infrastructure.Database;
    using Bizca.User.Domain.Entities.Address;
    using Bizca.User.Domain.Entities.Address.Repositories;
    using Bizca.User.Infrastructure.Extensions;
    using Dapper;
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

        public async Task<bool> SaveAsync(int userId, IEnumerable<Address> addresses)
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