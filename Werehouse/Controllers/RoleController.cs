using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Models;
using Warehouse.services;
namespace Warehouse.Controllers
{
    [Authorize(Policy = "Admin")]
    public class RoleController : Controller
    {
        IRoleServices iRoleServices;
        public RoleController (IRoleServices _iRoleServices)
        {
            iRoleServices= _iRoleServices;
        }
       public async Task<IActionResult> AddRolePage()
        {
            ViewData["Rolealert"] = false;
            return View("AddRole");

        }
        public async Task<IActionResult> AddRole(Role role)
        {
            ViewData["IsRemoved"] = true;

            await iRoleServices.NewRole(role);
            List<Role> Roles = new List<Role>();
            Roles = await iRoleServices.Getroles();
            if(Roles 
                != null) {
                return View("ViewRole", Roles);
            }
            else {

                ViewData["Rolealert"] = true;
                return View("AddRole");
            }
            
            

        }
        public async Task <IActionResult> ViewRoles()
        {
            ViewData["IsRemoved"] = true;
            List<Role> Roles = new List<Role>();
         Roles=await  iRoleServices.Getroles();
            return View("ViewRole", Roles);
        }


        public async Task<IActionResult> DeleteRole(string Name)
        {
            ViewData["IsRemoved"] = await iRoleServices.RemoveRole(Name);
            List<Role> Roles = new List<Role>();
            Roles = await iRoleServices.Getroles();
            return View("ViewRole", Roles);
        }


    } 
}
