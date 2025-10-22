
using System.ComponentModel.DataAnnotations;

namespace Warehouse.Models
{
    public class UserInfo
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        
        public string  RoleName { get; set; }
        public string id { get; set; }
        public List<GroupModel> Groups { get; set; }
        public bool IsSystemDefined { get; set; }
       

    }
}
