using Microsoft.AspNetCore.Identity;
using Warehouse.Models;

namespace Warehouse.services
{
    public interface IUserservices
    {
        Task<bool> CreateUser(UserInfo userInfo);

        Task<List<UserInfo>> GetActivateUsers();
        Task<bool> AddRole(string userId, string roleName);
        Task RemoveOldRole(string userId);
        Task DeactivateUser(string userId);
        Task ActivateUser(string userId);
        Task EditUserInfo(UserInfo updatedUser);
        Task<List<UserInfo>> GetDeatctivateUsers();
        Task<UserInfo> GetUserGroups(string userId);
      Task AssignUUserTOgroup(string userId, int groupId);
        Task DeleteUserFromGroup(string userId, int groupId);
        }
}
