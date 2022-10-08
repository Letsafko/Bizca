namespace Bizca.Core.Infrastructure.Persistence.Entity
{
    using Database;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table(Constant.Table.Reference.EmailTemplate, Schema = Constant.Schema.Reference)]
    public class EmailTemplateEntity
    {
        [Column("emailTemplateId")] public int EmailTemplateId { get; set; }

        [Column("emailTemplateTypeId")] public int EmailTemplateTypeId { get; set; }

        [Column("description")] public string Description { get; set; }
    }
}