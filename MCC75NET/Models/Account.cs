using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MCC75NET.Models
{
    [Table("tb_m_accounts")]

    public class Account
    {
        [Key, Column("employee_nik", TypeName = "nchar(5)")]
        public string EmployeeNik { get; set; }

        [Required, Column("password"),MaxLength(255)]
        public string Password { get; set; }

        //cardinality
        public ICollection<AccountRole>? AccountRoles { get; set; }
        public Employee? Employee { get; set; }


    }
}
