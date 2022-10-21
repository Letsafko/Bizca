namespace Bizca.Core.Infrastructure.Repository.Entity
{
    using Bizca.Core.Infrastructure.Database;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table(Constant.Table.Reference.EmailTemplate, Schema = Constant.Schema.Reference)]
    public class EmailTemplateEntity
    {
        [Column("emailTemplateId")] public int EmailTemplateId { get; init; }

        [Column("emailTemplateTypeId")] public int EmailTemplateTypeId { get; init; }

        [Column("description")] public string Description { get; init; }
    }
}