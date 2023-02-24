using System.ComponentModel.DataAnnotations;

namespace MCC75NET.ViewModels
{
    public class EducationUniversityVM
    {
        public int Id { get; set; }
        public string Major { get; set; }

        [MaxLength(2, ErrorMessage = "Contoh inputan ex: S1")]
        public string Degree { get; set; }

        [Range(0, 4, ErrorMessage = "Inputan harus lebih dari [1] dan kurang dari [2]")]
        public float Gpa { get; set; }

        [Display(Name = "University Name")]
        public string UniversityName { get; set; }

    }
}
