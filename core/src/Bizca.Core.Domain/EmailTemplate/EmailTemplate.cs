namespace Bizca.Core.Domain.EmailTemplate
{
    public sealed class EmailTemplate
    {
        public EmailTemplate(int emailTemplateId,
            EmailTemplateType emailTemplateType,
            string description)
        {
            EmailTemplateId = emailTemplateId;
            EmailTemplateType = emailTemplateType;
            Description = description;
        }

        public int EmailTemplateId { get; }
        public EmailTemplateType EmailTemplateType { get; }
        public string Description { get; }
    }
}
