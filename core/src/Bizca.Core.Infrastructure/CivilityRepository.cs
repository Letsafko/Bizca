namespace Bizca.Core.Infrastructure
{
    using Bizca.Core.Domain.Civility;
    using Bizca.Core.Infrastructure.Abstracts;
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
                    .QueryFirstAsync(getCivilityByIdStoredProcedure,
                            new { civilityId },
                            commandType: CommandType.StoredProcedure)
                    .ConfigureAwait(false);

            return new Civility(result.civilityId, result.civilityCode);
        }
    }
}
