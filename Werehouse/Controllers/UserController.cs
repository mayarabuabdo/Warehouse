using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Models;
using Warehouse.services;
namespace Warehouse.Controllers
{
    [Authorize(Policy = "Admin")]
    public class UserController : Controller
    {
        IUserservices userservices;
        IRoleServices roleServices;
        IGroupServices GroupServices;
        public UserController(IUserservices _userservices, IRoleServices _roleServices, IGroupServices groupServices)
        {
            userservices = _userservices;
            roleServices = _roleServices;
            GroupServices = groupServices;
        }
        public async Task<IActionResult> SignUpPage()
        {
          List<GroupModel> Groups     = await GroupServices.GetActivatedGroups();
            UserInfo user=new UserInfo();
           
            ViewData["create"] = true;
            return View("SignUpPage", user);
        }
        public async Task<IActionResult> SignUp(UserInfo userInfo)
        {
            ViewData["ActiveUser"] = true;
            var result= await  userservices.CreateUser(userInfo);
            ViewData["create"] = true;
            List<VMUser> Vmusers = new List<VMUser>();
            List<UserInfo> User = await userservices.GetActivateUsers();

            foreach (UserInfo user in User)
            {
                List<Role> UserRoles = await roleServices.Getroles();

                VMUser NewVmUser = new VMUser();
                NewVmUser.UserInfo = user;
                NewVmUser.RoleList = UserRoles;
                Vmusers.Add(NewVmUser);
            }

            return View("ViewUsers", Vmusers);

        }
        public async Task<IActionResult> EditUserPage(UserInfo userInfo)
        {
            ViewData["create"] = false;

            return View("SignUpPage", userInfo);

        }
        public async Task<IActionResult> EditUser(UserInfo userInfo)
        {
            ViewData["ActiveUser"] = true;
            ViewData["create"] = false;
         await userservices.EditUserInfo(userInfo);
            List<VMUser> Vmusers = new List<VMUser>();
            List<UserInfo> User = await userservices.GetActivateUsers();

            foreach (UserInfo user in User)
            {
        
                VMUser NewVmUser = new VMUser();
                NewVmUser.UserInfo = user;
             
                Vmusers.Add(NewVmUser);
            }

            return View("ViewUsers", Vmusers);

        }
        public async Task<IActionResult> ViewActiveUsers()
        {
            ViewData["ActiveUser"] = true;
            List< VMUser > Vmusers = new List< VMUser >();
            List<UserInfo> User= await userservices.GetActivateUsers();

            foreach (UserInfo userInfo in User) {
              List <Role> UserRoles  = await roleServices.Getroles();
              
                VMUser NewVmUser= new VMUser();
                NewVmUser.UserInfo = userInfo;
                NewVmUser.RoleList = UserRoles;
                Vmusers.Add(NewVmUser);
            }

            return View("ViewUsers", Vmusers);
        }
        public async Task<IActionResult> ViewDeactiveUsers()
        {
            ViewData["ActiveUser"] = false;
            List<VMUser> Vmusers = new List<VMUser>();
            List<UserInfo> User = await userservices.GetDeatctivateUsers();

            foreach (UserInfo userInfo in User)
            {
                List<Role> UserRoles = await roleServices.Getroles();

                VMUser NewVmUser = new VMUser();
                NewVmUser.UserInfo = userInfo;
                NewVmUser.RoleList = UserRoles;
                Vmusers.Add(NewVmUser);
            }

            return View("ViewUsers", Vmusers);
        }

        public async Task<IActionResult> AddUserRole(string userId , string roleName)
        {
            await userservices.RemoveOldRole(userId);
            bool isRoleAssigned =    await   userservices.AddRole( userId,  roleName);
      
            List<VMUser> Vmusers = new List<VMUser>();
            List<UserInfo> User = await userservices.GetActivateUsers();
            foreach (UserInfo userInfo in User)
            {
                VMUser NewVmUser = new VMUser();
                NewVmUser.UserInfo = userInfo;
           
                Vmusers.Add(NewVmUser);
            }
            return View("ViewUsers", Vmusers);
        }

        public async Task<IActionResult> DeactivateUser(string userId)
        {
            await userservices.DeactivateUser(userId);

            ViewData["ActiveUser"] = false;
            List<VMUser> Vmusers = new List<VMUser>();
            List<UserInfo> User = await userservices.GetDeatctivateUsers();
            foreach (UserInfo userInfo in User)
            {


                List<Role> UserRoles = await roleServices.Getroles();
                VMUser NewVmUser = new VMUser();
                NewVmUser.UserInfo = userInfo;
                NewVmUser.RoleList = UserRoles;
                Vmusers.Add(NewVmUser);
            }
            return View("ViewUsers", Vmusers);
        }
        public async Task<IActionResult> ActivateUser(string userId)
        {
            
            await userservices.ActivateUser(userId);
            ViewData["ActiveUser"] = true;
            List<VMUser> Vmusers = new List<VMUser>();
            List<UserInfo> User = await userservices.GetActivateUsers();
            foreach (UserInfo userInfo in User)
            {


                List<Role> UserRoles = await roleServices.Getroles();
                VMUser NewVmUser = new VMUser();
                NewVmUser.UserInfo = userInfo;
                NewVmUser.RoleList = UserRoles;
                Vmusers.Add(NewVmUser);
            }
            return View("ViewUsers", Vmusers);
        }
        public async Task<IActionResult> UserGroupPage(string userId)
        {
         UserInfo user=   await userservices.GetUserGroups(userId);
            VMUserInfo vMUser = new VMUserInfo();
            vMUser.Groups=   await  GroupServices.GetActivatedGroups();
            vMUser.UserInfo = user;

            return View("UserGroup", vMUser);
        }
        public async Task<IActionResult> AssignUserGroup(string userId, int groupId)
        {
              await userservices.AssignUUserTOgroup(userId, groupId);
            UserInfo user = await userservices.GetUserGroups(userId);
            VMUserInfo vMUser = new VMUserInfo();
            vMUser.Groups = await GroupServices.GetActivatedGroups();
            vMUser.UserInfo = user;

            return View("UserGroup", vMUser);

        }

        public async Task<IActionResult> DeleteUserGroup(string userId, int groupId)
        {
          await userservices.DeleteUserFromGroup(userId, groupId);
            UserInfo user = await userservices.GetUserGroups(userId);
            VMUserInfo vMUser = new VMUserInfo();
            vMUser.Groups = await GroupServices.GetActivatedGroups();
            vMUser.UserInfo = user;

            return View("UserGroup", vMUser);

        }


    }
}
