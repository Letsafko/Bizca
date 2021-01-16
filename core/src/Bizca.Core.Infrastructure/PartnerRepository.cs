namespace Bizca.Core.Infrastructure
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

        private readonly IUnitOfWork _unitOfWork;
        public PartnerRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private const string getPartnerByCodeStoredProcedure = "[ref].[usp_getByCode_partner]";

        #endregion

        public async Task<Partner> GetByCodeAsync(string partnerCode)
        {
            dynamic result = await _unitOfWork.Connection
                    .QueryFirstOrDefaultAsync(getPartnerByCodeStoredProcedure,
                            new { partnerCode },
                            _unitOfWork.Transaction,
                            commandType: CommandType.StoredProcedure)
                    .ConfigureAwait(false);

            return result is null
                    ? default
                    : new Partner(result.partnerId, result.partnerCode, result.description);
        }
    }
}
