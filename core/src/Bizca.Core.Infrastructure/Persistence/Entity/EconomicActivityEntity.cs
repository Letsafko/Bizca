namespace Bizca.Core.Infrastructure.Persistence.Entity
{
    using Database;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table(Constant.Table.Reference.EconomicActivity, Schema = Constant.Schema.Reference)]
    public class EconomicActivityEntity
    {
        [Key]
        [Column("economicActivityId")]
        public int Id { get; set; }
        
        [Column("economicActivityCode")]
        public string Code { get; set; }
        
        [Column("description")]
        public string Description { get; set; }
    }
}