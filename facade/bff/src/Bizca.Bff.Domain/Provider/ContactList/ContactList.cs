using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bizca.Bff.Domain.Provider.ContactList
{
    [Table("contactList", Schema = "bff")]
    public sealed class ContactList
    {
        [Key]
        [Column("listId")]
        public int ListId { get; set; }

        [Column("procedureTypeId")]
        public int ProcedureTypeId { get; set; }

        [Column("organismId")]
        public int OrganismId { get; set; }

        [Column("name")]
        public string Name { get; set; }
    }
}