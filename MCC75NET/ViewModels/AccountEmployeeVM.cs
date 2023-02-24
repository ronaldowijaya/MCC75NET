using System.ComponentModel.DataAnnotations;

namespace MCC75NET.ViewModels
{
    public class AccountEmployeeVM
    {
        [Display(Name = "Email")]
        [EmailAddress]
        public string EmployeeEmail { get; set; }

        public string Password { get; set; }
    }
}
