using System.ComponentModel.DataAnnotations;
using soft.Models;

namespace soft.Models
{
    public class Register
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]

        public string ConfirmPassword { get; set; }

        public Role Roles { get; set; }

        public enum Role{
            Admin,
            Teacher,
            Student
        }
    }
}
