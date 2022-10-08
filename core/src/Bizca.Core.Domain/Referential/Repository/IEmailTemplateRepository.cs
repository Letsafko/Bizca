namespace Bizca.Core.Domain.Referential.Repository
{
    using Model;
    using System.Threading.Tasks;

    public interface IEmailTemplateRepository
    {
        Task<EmailTemplate> GetByIdAsync(int emailTemplateTypeId, string languageCode = "fr");
    }
}
