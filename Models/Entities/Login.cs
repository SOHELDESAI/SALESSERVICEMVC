using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesService.Models.Entities
{
    [Table("Login")]
    public class Login
    {
        [Key]
        [Required]
        [StringLength(50)]
        public string EmployeeId { get; set; }

        [Required]
        [StringLength(50)]
        [Column("Login")]
        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        [Column("Pwd")]
        public string Password { get; set; }

        public bool IsActive { get; set; }
    }
}