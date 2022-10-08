namespace Bizca.Core.Domain.Referential.Model
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

        public EmailTemplateType EmailTemplateType { get; }
        public int EmailTemplateId { get; }
        public string Description { get; }
    }
}