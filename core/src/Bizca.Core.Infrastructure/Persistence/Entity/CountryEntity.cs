namespace Bizca.Core.Infrastructure.Persistence.Entity
{
    using Database;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table(Constant.Table.Reference.Country, Schema = Constant.Schema.Reference)]
    public class CountryEntity
    {
        [Key] [Column("countryId")] public int Id { get; set; }

        [Column("countryCode")] public string Code { get; set; }

        [Column("description")] public string Description { get; set; }
    }
}