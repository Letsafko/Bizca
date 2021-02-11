namespace Bizca.Core.Infrastructure.Persistance
{
    using Bizca.Core.Domain;
    using Bizca.Core.Domain.Civility;
    using Dapper;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;

    [ExcludeFromCodeCoverage]
    public sealed class CivilityRepository : ICivilityRepository
    {
        #region fields, const & ctor

        private readonly IUnitOfWork unitOfWork;
        public CivilityRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        private const string getCivilityByIdStoredProcedure = "[ref].[usp_getById_civility]";

        #endregion

        public async Task<Civility> GetByIdAsync(int civilityId)
        {
            var parameters = new
            {
                civilityId
            };

            dynamic result = await unitOfWork.Connection
                    .QueryFirstOrDefaultAsync(getCivilityByIdStoredProcedure,
                            parameters,
                            unitOfWork.Transaction,
                            commandType: CommandType.StoredProcedure)
                    .ConfigureAwait(false);

            return result is null
                    ? default
                    : new Civility(result.civilityId, result.civilityCode);
        }
    }
}