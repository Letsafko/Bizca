namespace Bizca.Core.Infrastructure
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

        private readonly IUnitOfWork _unitOfWork;
        public CivilityRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private const string getCivilityByIdStoredProcedure = "[ref].[usp_getById_civility]";

        #endregion

        public async Task<Civility> GetByIdAsync(int civilityId)
        {
            dynamic result = await _unitOfWork.Connection
                    .QueryFirstOrDefaultAsync(getCivilityByIdStoredProcedure,
                            new { civilityId },
                            _unitOfWork.Transaction,
                            commandType: CommandType.StoredProcedure)
                    .ConfigureAwait(false);

            return result is null
                    ? default
                    : new Civility(result.civilityId, result.civilityCode);
        }
    }
}
