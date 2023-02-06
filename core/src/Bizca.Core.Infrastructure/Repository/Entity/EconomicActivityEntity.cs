namespace Bizca.Core.Infrastructure.Repository.Entity
{
    using Bizca.Core.Infrastructure.Database;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table(Constant.Table.Reference.EconomicActivity, Schema = Constant.Schema.Reference)]
    public class EconomicActivityEntity
    {
        [Key] [Column("economicActivityId")] public int Id { get; init; }

        [Column("economicActivityCode")] public string Code { get; init; }

        [Column("description")] public string Description { get; init; }
    }
}