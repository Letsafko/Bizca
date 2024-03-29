﻿namespace Bizca.User.Infrastructure.Persistance
{
    using Bizca.Core.Domain;
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
        private readonly IUnitOfWork unitOfWork;
        public AddressRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        private const string upSertAddressesStoredProcedure = "[usr].[usp_upsert_address]";

        public async Task<bool> UpsertAsync(int userId, IEnumerable<Address> addresses)
        {
            var parameters = new
            {
                addresses = new TableValueParameter(addresses.ToDataTable(userId))
            };

            return await unitOfWork.Connection
                .ExecuteAsync(upSertAddressesStoredProcedure,
                    parameters,
                    unitOfWork.Transaction,
                    commandType: CommandType.StoredProcedure)
                .ConfigureAwait(false) > 0;
        }
    }
}