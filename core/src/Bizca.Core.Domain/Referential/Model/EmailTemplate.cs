namespace Bizca.Core.Domain.Referential.Model
{
    using Enums;
    using System.Collections.Generic;

    public sealed class EmailTemplate : ValueObject
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
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return EmailTemplateType;
            yield return EmailTemplateId;
        }
    }
}