﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCC75NET.Models
{
    [Table("tb_m_educations")]
    public class Education
    {
        [Key, Column("id")]
        public int Id { get; set; }

        [Required,Column("major"), MaxLength(100)]
        public string Major { get; set; }

        [Required, Column("degree", TypeName = "nchar(5)")]
        public string Degree { get; set; }

        [Required, Column("gpa")]
        public float Gpa { get; set; }

        [Required, Column("university_id")]
        public int UniversityId { get; set; }

        //relasi
        [ForeignKey(nameof(UniversityId))] 
        //cardinality
        public University? University { get; set; }
        public ICollection<Profiling>? Profilings { get; set; }

    }
}
