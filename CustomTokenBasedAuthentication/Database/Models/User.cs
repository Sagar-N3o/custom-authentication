using System.ComponentModel.DataAnnotations;

namespace CustomTokenBasedAuthentication.Database.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Maximum 50 character allowed for FirstName")]
        public string FirstName { get; set; }

        [StringLength(50, ErrorMessage = "Maximum 50 character allowed for LastName")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please Enter Email Address")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
            ErrorMessage = "Please Enter Correct Email Address")]
        public string Email { get; set; }

        public int RoleId { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }

        public Role Role { get; set; }
    }
}
