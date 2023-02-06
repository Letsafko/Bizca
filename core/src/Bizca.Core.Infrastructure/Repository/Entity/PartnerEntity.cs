namespace Bizca.Core.Infrastructure.Repository.Entity
{
    using Database;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table(Constant.Table.Reference.Partner, Schema = Constant.Schema.Reference)]
    public class PartnerEntity
    {
        [Key] [Column("partnerId")] public int Id { get; init; }

        [Column("partnerCode")] public string Code { get; init; }

        [Column("description")] public string Description { get; init; }
    
        [Column("configuration")] public string Configuration { get; init; }
    }
}