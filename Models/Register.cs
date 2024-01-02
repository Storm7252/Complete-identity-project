using System.ComponentModel.DataAnnotations;
using soft.Models;

namespace soft.Models
{
    public class Register
    {
        [Required(ErrorMessage ="please enter Username")]
        public string Username { get; set; }

        [Required(ErrorMessage ="please enter email address")]
        [EmailAddress(ErrorMessage ="please enter valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage ="please enter your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password,ErrorMessage ="please enter your password")]
        [Compare("Password",ErrorMessage ="Password doesnt match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage ="please select roles")]
        public Role Roles { get; set; }

        public enum Role{
            Admin,
            Teacher,
            Student
        }
    }
}
