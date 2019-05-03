using System.ComponentModel.DataAnnotations;

namespace CustomTokenBasedAuthentication.Database.Models
{
    public class Role
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Maximum 50 character allowed for Role Name")]
        public string Name { get; set; }
    }
}