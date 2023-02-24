using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MCC75NET.Models
{
    [Table("tb_tr_account_roles")]

    public class AccountRole
    {
        [Key, Column("id")]
        public int Id { get; set; }

        [Required, Column("account_nik", TypeName = "nchar(5)")]
        public string AccountNik { get; set; }

        [Required, Column("role_id")]
        public int RoleId { get; set; }
        
        //relasi
        [ForeignKey(nameof(AccountNik))]
        //cardinality
        public Account? Account { get; set; }

        //relasi
        [ForeignKey(nameof(RoleId))]
        //cardinality
        public Role? Role { get; set; }

    }
}
