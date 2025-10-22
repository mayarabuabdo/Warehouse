using Warehouse.Models;

namespace Warehouse.services
{
    public interface IGroupServices
    {
        Task DeactiveGroup(int groupId);
        Task<List<GroupModel>> GetActivatedGroups();
        Task NewGroup(GroupModel group);
        Task<List<GroupModel>> GetDeactivatedGroups();
        Task ActiveGroup(int groupId);
    }
}