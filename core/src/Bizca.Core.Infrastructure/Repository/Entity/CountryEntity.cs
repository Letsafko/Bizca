namespace Bizca.Core.Infrastructure.Repository.Entity
{
    using Bizca.Core.Infrastructure.Database;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table(Constant.Table.Reference.Country, Schema = Constant.Schema.Reference)]
    public class CountryEntity
    {
        [Key] [Column("countryId")] public int Id { get; init; }

        [Column("countryCode")] public string Code { get; init; }

        [Column("description")] public string Description { get; init; }
    }
}