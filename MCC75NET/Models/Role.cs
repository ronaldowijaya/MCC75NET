using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MCC75NET.Models
{
    [Table("tb_m_roles")]
    public class Role
    {
        [Key, Column("id")]
        public int Id { get; set; }

        [Required, Column("name"), MaxLength(50)]
        public string Name { get; set; }

        //cardinality
        public ICollection<AccountRole>? AccountRoles { get; set; }

    }
}
