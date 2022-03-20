namespace Bizca.Bff.Domain.Provider.Folder
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("folder", Schema = "bff")]
    public sealed class Folder
    {
        [Key]
        [Column("folderId")]
        public int FolderId { get; set; }

        [Column("defaultListId")]
        public int ListId { get; set; }

        [Column("partnerId")]
        public int PartnerId { get; set; }

        [Column("name")]
        public string Name { get; set; }
    }
}
