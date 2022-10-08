namespace Bizca.User.Infrastructure.Persistance
{
    using Core.Infrastructure.Database;
    using Domain.Agregates.Repositories;
    using Domain.Agregates.ValueObjects;
    using Extensions;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;

    public sealed class PasswordRepository : IPasswordRepository
    {
        private const string RegisterPasswordStoredProcedure = "[usr].[usp_create_password]";
        private readonly IUnitOfWork unitOfWork;

        public PasswordRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> AddAsync(int userId, ICollection<Password> passwords)
        {
            var parameters = new { passwords = new TableValueParameter(passwords.ToDataTable(userId)) };

            return await unitOfWork.Connection
                .ExecuteAsync(RegisterPasswordStoredProcedure,
                    parameters,
                    unitOfWork.Transaction,
                    commandType: CommandType.StoredProcedure)
                .ConfigureAwait(false) > 0;
        }
    }
}