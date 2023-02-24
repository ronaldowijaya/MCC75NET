using MCC75NET.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCC75NET.ViewModels
{
    public class EmployeeVM
    {       
        public string Nik { get; set; }

        [Display(Name = "First Name"), MaxLength(50)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name"), MaxLength(50)]
        public string? LastName { get; set; }

        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }

        public GenderEnum Gender { get; set; }

        [Display(Name = "Hiring Date")]
        public DateTime HiringDate { get; set; }

        [EmailAddress,Display(Name = "Email"), MaxLength(50)]
        public string Email { get; set; }

        [Phone,Display(Name = "Phone Number"), MaxLength(20)]
        public string? PhoneNumber { get; set; }
    }
}
