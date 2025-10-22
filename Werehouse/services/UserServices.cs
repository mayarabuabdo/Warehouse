using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Warehouse.Data;
using Warehouse.Models;
using Warehouse.Data;

namespace Warehouse.services
{
    public class UserServices: IUserservices
    {
        WarehouseContext warehouseContext;
        UserManager<AppUser> userManager;
        IRoleServices roleServices;
        SignInManager<AppUser> signInManager;
        public UserServices(WarehouseContext _warehouseContext, UserManager<AppUser> _userManager, IRoleServices _roleServices, SignInManager<AppUser> _signInManager)
        {
           warehouseContext = _warehouseContext;
           userManager = _userManager;
            roleServices= _roleServices;
            this.signInManager = _signInManager;
        }

        public async Task<bool> CreateUser(UserInfo userInfo)
        {
         AppUser Existuser=    userManager.Users.FirstOrDefault(u=> u.Email== userInfo.Email && u.FirstName== userInfo.FirstName && u.LastName==userInfo.LastName);

            if (Existuser == null)
            {
                AppUser user = new AppUser();
                user.Email = userInfo.Email;
                user.UserName = userInfo.Email;
                user.FirstName = userInfo.FirstName;
                user.LastName = userInfo.LastName;
                user.IsActive = true;
                user.IsSystemDefine = false;

                var creationResult = await userManager.CreateAsync(user, userInfo.Password);

                if (creationResult.Succeeded) {
              
                return true;
                
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }


        }

        public async Task<List<UserInfo>> GetActivateUsers()
        {
           List<AppUser> IdentityUsers= userManager.Users.Include(u=>u.UserGroups).ThenInclude(u=>u.Group).Where(u=>u.IsActive==true).ToList();
            List<UserInfo> Users= new List<UserInfo>();
             foreach (var user in IdentityUsers) {


                UserInfo userInfo = new UserInfo()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    id = user.Id,
                    Groups = new List<GroupModel>(),
                    IsSystemDefined=user.IsSystemDefine
                    

                };
                List<Group> Groups = user.UserGroups.Select(g => g.Group).ToList();
                foreach (Group group in Groups)
                {
                    GroupModel groupM = new GroupModel()
                    {
                        Id = group.Id,
                        Name = group.Name
                    };

                    userInfo.Groups.Add(groupM);
                }

                Users.Add(userInfo);
            }

            return Users;

        }
        public async Task<List<UserInfo>> GetDeatctivateUsers()
        {
            List<AppUser> IdentityUsers = userManager.Users.Include(u => u.UserGroups).ThenInclude(u => u.Group).Where(u => u.IsActive == false).ToList();
            List<UserInfo> Users = new List<UserInfo>();
            foreach (var user in IdentityUsers)
            {


                UserInfo userInfo = new UserInfo()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    id = user.Id,
                    Groups = new List<GroupModel>(),
                     IsSystemDefined = user.IsSystemDefine

                };
                List<Group> Groups = user.UserGroups.Select(g => g.Group).ToList();
                foreach (Group group in Groups)
                {
                    GroupModel groupM = new GroupModel()
                    {
                        Id = group.Id,
                        Name = group.Name
                    };

                    userInfo.Groups.Add(groupM);
                }

                Users.Add(userInfo);
            }

            return Users;

        }

        public async Task<bool> AddRole(string userId , string roleName)
        {
            AppUser user= userManager.Users.FirstOrDefault(u => u.Id == userId);
            IdentityResult result= await userManager.AddToRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        
       public async Task RemoveOldRole(string userId)
        {
              AppUser user=    userManager.Users.FirstOrDefault(u=>u.Id==userId);
            var oldRoles = await userManager.GetRolesAsync(user);

           
            if (oldRoles.Any())
            {
                var removeResult = await userManager.RemoveFromRolesAsync(user, oldRoles);
              
            }

        }
       public async Task DeactivateUser(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user != null) {

                user.IsActive = false;
                warehouseContext.SaveChanges();
            }

        }
        public async Task ActivateUser(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user != null) {
                user.IsActive = true;
                warehouseContext.SaveChanges();
            }
               
        }
        public async Task EditUserInfo(UserInfo updatedUser)
        {
            AppUser user= await userManager.Users.FirstOrDefaultAsync(  u=>u.Id== updatedUser.id);   
            if (user == null)
            {

            }
            else
            {
                user.FirstName = updatedUser.FirstName;
                user.LastName = updatedUser.LastName;
                user.Email = updatedUser.Email;
                await userManager.UpdateAsync(user);
            }
        }
       public async Task<UserInfo> GetUserGroups(string userId)
       {
            var user = userManager.Users
    .Include(u => u.UserGroups)
        .ThenInclude(ug => ug.Group)
    .FirstOrDefault(u => u.Id == userId);
    

            UserInfo userInfo = new UserInfo();
            if (user != null) {
                userInfo.id = user.Id;
                userInfo.FirstName=user.FirstName;
                userInfo.LastName=user.LastName;
                userInfo.Groups = new List<GroupModel>();
                List<Group> Groups = user.UserGroups.Select(g => g.Group).ToList();
                foreach (Group group in Groups)
                {
                    GroupModel groupM = new GroupModel()
                    {
                        Id = group.Id,
                        Name = group.Name
                    };

                    userInfo.Groups.Add(groupM);
                }

            }

          return userInfo;


        }
       public async Task AssignUUserTOgroup(string userId, int groupId)
       {
         bool isUserExist=   userManager.Users.Any(u => u.Id == userId);

         bool isGroupExist = warehouseContext.Groups.Any(g => g.Id == groupId);
         if(isUserExist && isGroupExist)
         {
                bool isUserGroupExist= warehouseContext.UserGroup.Any(u=>u.UserId==userId && u.GroupId==groupId);
                if (!isUserGroupExist) {
                    UserGroup newUserGroup = new UserGroup()
                    {
                        UserId = userId,
                        GroupId = groupId
                    };

                    warehouseContext.UserGroup.Add(newUserGroup);
                    warehouseContext.SaveChanges();
                }
           

            }


        }
       public async Task DeleteUserFromGroup(string userId, int groupId)
       {
            bool isUserExist = userManager.Users.Any(u => u.Id == userId);

            bool isGroupExist = warehouseContext.Groups.Any(g => g.Id == groupId);
            if (isUserExist && isGroupExist)
            {
                UserGroup UserGroupExist = warehouseContext.UserGroup.FirstOrDefault(u => u.UserId == userId && u.GroupId == groupId);
                if (UserGroupExist != null)
                {
                    

                    warehouseContext.UserGroup.Remove(UserGroupExist);
                    warehouseContext.SaveChanges();
                }


            }
        }
    }
}
