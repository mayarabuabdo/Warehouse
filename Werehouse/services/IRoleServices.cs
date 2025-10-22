using Microsoft.AspNetCore.Identity;
using Warehouse.Models;

namespace Warehouse.services
{
    public interface IRoleServices
    {
        Task NewRole(Role role);
        Task<List<Role>> Getroles();
        Task<bool> RemoveRole(string Name);
    }
}
