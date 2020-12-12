namespace Bizca.Core.Infrastructure
{
    using Bizca.Core.Domain.EconomicActivity;
    using Bizca.Core.Domain.Repositories;
    using Dapper;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;

    [ExcludeFromCodeCoverage]
    public sealed class EconomicActivityRepository : IEconomicActivityRepository
    {
        #region fields, const & ctor

        private readonly IUnitOfWork _unitOfWork;
        public EconomicActivityRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private const string getEconomicActivityByIdStoredProcedure = "[ref].[usp_getById_economicActivity]";

        #endregion

        public async Task<EconomicActivity> GetByIdAsync(int economicActivityId)
        {
            dynamic result = await _unitOfWork.Connection
                    .QueryFirstAsync(getEconomicActivityByIdStoredProcedure,
                            new { economicActivityId },
                            commandType: CommandType.StoredProcedure)
                    .ConfigureAwait(false);

            return new EconomicActivity(result.economicActivityId, result.economicActivityCode, result.description);
        }
    }
}
