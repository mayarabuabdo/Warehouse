using Microsoft.AspNetCore.Identity;
using Warehouse.Models;
namespace Warehouse.services
{
    public class RoleServices: IRoleServices
    {
        RoleManager<IdentityRole> roleManager;
        public RoleServices(RoleManager<IdentityRole> _roleManager)
        {
            this.roleManager = _roleManager;
        }
        public async Task NewRole(Role role)
        {
            bool roleExists = await roleManager.RoleExistsAsync(role.Name);
            if (!roleExists) {
                IdentityRole newRole = new IdentityRole();
                newRole.Name = role.Name;
                IdentityResult creationResult = await roleManager.CreateAsync(newRole);
               
            }
         
        }
        public async Task<List<Role>> Getroles()
        {
           List<IdentityRole> Roles =  roleManager.Roles.ToList();
            List<Role> roles = new List<Role>(); 
            if(Roles != null) {
                foreach (IdentityRole role in Roles)
                {
                    Role newRole = new Role();
                    newRole.Name = role.Name;
                    roles.Add(newRole);
                }
            }
        
            return roles;
        }

        public async Task<bool> RemoveRole(string Name)
        {
            var role = await roleManager.FindByNameAsync(Name);

            
            var result = await roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return true;
             }
            
            else
            {
                return false;
            }




        }







    }
}
