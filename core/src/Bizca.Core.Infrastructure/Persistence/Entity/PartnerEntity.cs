namespace Bizca.Core.Infrastructure.Persistence.Entity
{
    using Database;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table(Constant.Table.Reference.Partner, Schema = Constant.Schema.Reference)]
    public class PartnerEntity
    {
        [Key]
        [Column("partnerId")]
        public int Id { get; set; }
        
        [Column("partnerCode")]
        public string Code { get; set; }
        
        [Column("description")]
        public string Description { get; set; }
    }
}