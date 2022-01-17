namespace Bizca.Core.Domain.EmailTemplate
{
    using System.Threading.Tasks;
    public interface IEmailTemplateRepository
    {
        Task<EmailTemplate> GetByIdAsync(int emailTemplateTypeId, string languageCode = "fr");
    }
}
