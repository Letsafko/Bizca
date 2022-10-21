namespace Bizca.Core.Infrastructure.Repository.Entity
{
    using Bizca.Core.Infrastructure.Database;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table(Constant.Table.Reference.Civility, Schema = Constant.Schema.Reference)]
    public class CivilityEntity
    {
        [Column("civilityId")] public int Id { get; init; }

        [Column("civilityCode")] public string Code { get; init; }
    }
}