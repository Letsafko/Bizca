namespace Bizca.Core.Infrastructure
{
    using Bizca.Core.Domain.Country;
    using Bizca.Core.Domain.Repositories;
    using Dapper;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;

    [ExcludeFromCodeCoverage]
    public sealed class CountryRepository : ICountryRepository
    {
        #region fields, const & ctor

        private readonly IUnitOfWork _unitOfWork;
        public CountryRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private const string getCountryByCodeStoredProcedure = "[ref].[usp_getByCode_country]";

        #endregion

        public async Task<Country> GetByCodeAsync(string countryCode)
        {
            dynamic result = await _unitOfWork.Connection
                    .QueryFirstAsync(getCountryByCodeStoredProcedure,
                            new { countryCode },
                            commandType: CommandType.StoredProcedure)
                    .ConfigureAwait(false);

            return new Country(result.countryId, result.countryCode, result.description);
        }
    }
}
