namespace Bizca.Core.Infrastructure.Persistence
{
    using Dapper;
    using Domain.Referential.Model;
    using Domain.Referential.Repository;
    using Entity;
    using System.Data;
    using System.Threading.Tasks;

    public sealed class EmailTemplateRepository : BaseRepository<EmailTemplateEntity>, IEmailTemplateRepository
    {
        private const string GetEmailTemplateByIdStoredProcedure = "[ref].[usp_getByType_emailTemplate]";

        public EmailTemplateRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<EmailTemplate> GetByIdAsync(int emailTemplateTypeId, string languageCode = "fr")
        {
            var parameters = new { emailTemplateTypeId, languageCode };

            EmailTemplateEntity result = await UnitOfWork.Connection
                .QueryFirstOrDefaultAsync<EmailTemplateEntity>(GetEmailTemplateByIdStoredProcedure,
                    parameters,
                    UnitOfWork.Transaction,
                    commandType: CommandType.StoredProcedure)
                .ConfigureAwait(false);

            return result is null
                ? default
                : new EmailTemplate(result.EmailTemplateId,
                    (EmailTemplateType)result.EmailTemplateTypeId,
                    result.Description);
        }
    }
}