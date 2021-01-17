namespace Bizca.Core.Infrastructure.Persistance
{
    using Bizca.Core.Domain;
    using Bizca.Core.Domain.Partner;
    using Dapper;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;

    [ExcludeFromCodeCoverage]
    public sealed class PartnerRepository : IPartnerRepository
    {
        #region fields, const & ctor

        private readonly IUnitOfWork unitOfWork;
        public PartnerRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        private const string getPartnerByCodeStoredProcedure = "[ref].[usp_getByCode_partner]";

        #endregion

        public async Task<Partner> GetByCodeAsync(string partnerCode)
        {
            var parameters = new
            {
                partnerCode
            };

            dynamic result = await unitOfWork.Connection
                    .QueryFirstOrDefaultAsync(getPartnerByCodeStoredProcedure,
                            parameters,
                            unitOfWork.Transaction,
                            commandType: CommandType.StoredProcedure)
                    .ConfigureAwait(false);

            return result is null
                    ? default
                    : new Partner(result.partnerId, result.partnerCode, result.description);
        }
    }
}