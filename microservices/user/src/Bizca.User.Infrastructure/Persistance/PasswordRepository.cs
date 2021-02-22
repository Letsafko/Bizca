namespace Bizca.User.Infrastructure.Persistance
{
    using Bizca.Core.Domain;
    using Bizca.Core.Infrastructure.Database;
    using Bizca.User.Domain.Agregates.Repositories;
    using Bizca.User.Domain.Agregates.ValueObjects;
    using Bizca.User.Infrastructure.Extensions;
    using Dapper;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;

    public sealed class PasswordRepository : IPasswordRepository
    {
        private readonly IUnitOfWork unitOfWork;
        public PasswordRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        private const string RegisterPasswordStoredProcedure = "[usr].[usp_create_password]";

        public async Task<bool> AddAsync(int userId, ICollection<Password> passwords)
        {
            var parameters = new
            {
                passwords = new TableValueParameter(passwords.ToDataTable(userId))
            };

            return await unitOfWork.Connection
                .ExecuteAsync(RegisterPasswordStoredProcedure,
                    parameters,
                    unitOfWork.Transaction,
                    commandType: CommandType.StoredProcedure)
                .ConfigureAwait(false) > 0;
        }
    }
}