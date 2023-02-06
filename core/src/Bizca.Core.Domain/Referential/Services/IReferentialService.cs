namespace Bizca.Core.Domain.Referential.Services
{
    using Model;
    using System.Threading.Tasks;

    public interface IReferentialService
    {
        Task<EconomicActivity> GetEconomicActivityByIdAsync(int economicActivity, bool throwError = false);
        Task<EmailTemplate> GetEmailTemplateByIdAsync(int emailTemplate, bool throwError = false);
        Task<Country> GetCountryByCodeAsync(string countryCode, bool throwError = false);
        Task<Partner> GetPartnerByCodeAsync(string partner, bool throwError = false);
        Task<Civility> GetCivilityByIdAsync(int civility, bool throwError = false);
        Task<Country> GetCountryByIdAsync(int countryId, bool throwError = false);
    }
}