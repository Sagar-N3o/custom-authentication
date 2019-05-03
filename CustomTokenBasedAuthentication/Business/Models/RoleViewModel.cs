using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomTokenBasedAuthentication.Business.Models
{
    public class RoleViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Maximum 50 character allowed for Role Name")]
        public string Name { get; set; }

        public ICollection<UserViewModel> UserViewModels { get; set; }
    }
}
