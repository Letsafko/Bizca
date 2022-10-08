namespace Bizca.Core.Infrastructure.Persistence.Entity
{
    using Database;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table(Constant.Table.Reference.Civility, Schema = Constant.Schema.Reference)]
    public class CivilityEntity
    {
        [Column("civilityId")]
        public int Id { get; set; }
        
        [Column("civilityCode")]
        public string Code { get; set; }
    }
}