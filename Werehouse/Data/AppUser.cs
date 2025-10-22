using Microsoft.AspNetCore.Identity;

namespace Warehouse.Data
{
    public class AppUser : IdentityUser
    {
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public List<UserGroup> UserGroups { get; set; }
      public bool IsActive { get; set; }
      public bool IsSystemDefine { get; set; }

    }
}
