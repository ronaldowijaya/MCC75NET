using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MCC75NET.Models
{
    [Table("tb_tr_profilings")]
    public class Profiling
    {
        [Key, Column("id")]
        public int Id { get; set; }

        [Required, Column("employee_nik", TypeName = "nchar(5)")]
        public string EmployeeNik { get; set; }

        [Required, Column("education_id")]
        public int EducationId { get; set; }

        //relasi
        [ForeignKey(nameof(EducationId))]
        //cardinality
        public Education? Education { get; set; }

        //relasi
        [ForeignKey(nameof(EmployeeNik))]
        //cardinality
        public Employee? Employee { get; set; }


    }
}
