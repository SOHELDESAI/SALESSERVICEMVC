using System.ComponentModel.DataAnnotations;

namespace SalesService.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please select a company")]
        [Display(Name = "Company")]
        public string Company { get; set; }

        [Required(ErrorMessage = "Please select a database")]
        [Display(Name = "Database")]
        public string Database { get; set; }

        public bool RememberMe { get; set; }
    }

    public class LoginResult
    {
        public bool Success { get; set; }
        public string EmployeeId { get; set; }
        public string Username { get; set; }
        public string Message { get; set; }
    }
}