namespace Bizca.Core.Infrastructure.Persistance
{
    using Bizca.Core.Domain;
    using Bizca.Core.Domain.EconomicActivity;
    using Dapper;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;

    [ExcludeFromCodeCoverage]
    public sealed class EconomicActivityRepository : IEconomicActivityRepository
    {
        #region fields, const & ctor

        private readonly IUnitOfWork unitOfWork;
        public EconomicActivityRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        private const string getEconomicActivityByIdStoredProcedure = "[ref].[usp_getById_economicActivity]";

        #endregion

        public async Task<EconomicActivity> GetByIdAsync(int economicActivityId)
        {
            var parameters = new
            {
                economicActivityId
            };

            dynamic result = await unitOfWork.Connection
                    .QueryFirstOrDefaultAsync(getEconomicActivityByIdStoredProcedure,
                            parameters,
                            unitOfWork.Transaction,
                            commandType: CommandType.StoredProcedure)
                    .ConfigureAwait(false);

            return result is null
                    ? default
                    : new EconomicActivity(result.economicActivityId, result.economicActivityCode, result.description);
        }
    }
}