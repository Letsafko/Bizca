namespace Bizca.Core.Infrastructure.Persistance
{
    using Bizca.Core.Domain;
    using Bizca.Core.Domain.Country;
    using Dapper;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;

    [ExcludeFromCodeCoverage]
    public sealed class CountryRepository : ICountryRepository
    {
        #region fields, const & ctor

        private readonly IUnitOfWork unitOfWork;
        public CountryRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        private const string getCountryByCodeStoredProcedure = "[ref].[usp_getByCode_country]";

        #endregion

        public async Task<Country> GetByCodeAsync(string countryCode)
        {
            var parameters = new
            {
                countryCode
            };

            dynamic result = await unitOfWork.Connection
                    .QueryFirstOrDefaultAsync(getCountryByCodeStoredProcedure,
                            parameters,
                            unitOfWork.Transaction,
                            commandType: CommandType.StoredProcedure)
                    .ConfigureAwait(false);

            return result is null
                    ? default
                    : new Country(result.countryId, result.countryCode, result.description);
        }
    }
}