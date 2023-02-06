namespace Bizca.Core.Infrastructure.Repository.Entity
{
    using Database;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table(Constant.Table.Reference.Civility, Schema = Constant.Schema.Reference)]
    public class CivilityEntity
    {
        [Key] [Column("civilityId")] public int Id { get; init; }

        [Column("civilityCode")] public string Code { get; init; }
    }
}