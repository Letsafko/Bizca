namespace Bizca.Core.Domain.Services
{
    using System.Threading.Tasks;

    public interface IReferentialService
    {
        Task<Civility.Civility> GetCivilityByIdAsync(int civility, bool throwError = false);
        Task<Partner.Partner> GetPartnerByCodeAsync(string partner, bool throwError = false);
        Task<Country.Country> GetCountryByIdAsync(int countryId, bool throwError = false);
        Task<Country.Country> GetCountryByCodeAsync(string countryCode, bool throwError = false);
        Task<EconomicActivity.EconomicActivity> GetEconomicActivityByIdAsync(int economicActivity, bool throwError = false);
    }
}