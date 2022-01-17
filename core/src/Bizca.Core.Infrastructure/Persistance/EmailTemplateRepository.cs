namespace Bizca.Core.Infrastructure.Persistance
{
    using Bizca.Core.Domain;
    using Bizca.Core.Domain.EmailTemplate;
    using Dapper;
    using System.Data;
    using System.Threading.Tasks;

    public sealed class EmailTemplateRepository : IEmailTemplateRepository
    {
        #region fields, const & ctor

        private readonly IUnitOfWork unitOfWork;
        public EmailTemplateRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        private const string getEmailTemplateByIdStoredProcedure = "[ref].[usp_getByType_emailTemplate]";
        public async Task<EmailTemplate> GetByIdAsync(int emailTemplateTypeId, string languageCode = "fr")
        {
            var parameters = new
            {
                emailTemplateTypeId,
                languageCode
            };

            dynamic result = await unitOfWork.Connection
                    .QueryFirstOrDefaultAsync(getEmailTemplateByIdStoredProcedure,
                            parameters,
                            unitOfWork.Transaction,
                            commandType: CommandType.StoredProcedure)
                    .ConfigureAwait(false);

            return result is null
                    ? default
                    : new EmailTemplate(result.emailTemplateId,
                        (EmailTemplateType)result.emailTemplateTypeId,
                        result.description);
        }

        #endregion
    }
}
