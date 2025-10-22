using System.Linq;
using Warehouse.Data;
using Warehouse.Models;
namespace Warehouse.services
{
    public class GroupServices : IGroupServices
    {
        WarehouseContext warehouseContext;
        public GroupServices(WarehouseContext _warehouseContext)
        {
            warehouseContext = _warehouseContext;
        }

        public async Task NewGroup(GroupModel group)
        {
            if (group != null)
            {

                Group group1 = new Group()
                {
                    Name = group.Name,
                    IsSystemDefine = false,
                    IsActive=true
                };

                warehouseContext.Groups.Add(group1);
                warehouseContext.SaveChanges();
            }

        }

        public async Task<List<GroupModel>> GetActivatedGroups()
        {
            List<Group> groups = warehouseContext.Groups.Where(g=>g.IsActive==true).ToList();
            List<GroupModel> groupModels = new List<GroupModel>();
            if (groups != null)
            {

                foreach (Group group in groups)
                {
                    GroupModel groupModel = new GroupModel();
                    groupModel.Name = group.Name;
                    groupModel.Id = group.Id;
                    groupModel.IsSystemDefined = group.IsSystemDefine;
                    groupModels.Add(groupModel);
                  
                }
            }
            return groupModels;

        }
        public async Task<List<GroupModel>> GetDeactivatedGroups()
        {
            List<Group> groups = warehouseContext.Groups.Where(g => g.IsActive == false).ToList();
            List<GroupModel> groupModels = new List<GroupModel>();
            if (groups != null)
            {

                foreach (Group group in groups)
                {
                    GroupModel groupModel = new GroupModel();
                    groupModel.Name = group.Name;
                    groupModel.Id = group.Id;
                    groupModels.Add(groupModel);
                }
            }
            return groupModels;

        }
        public async Task DeactiveGroup(int groupId)
        {
            Group groupToDelete = warehouseContext.Groups.FirstOrDefault(g => g.Id == groupId);
            if (groupToDelete != null)
            {
                groupToDelete.IsActive = false;
                warehouseContext.SaveChanges();


            }
        }
        public async Task ActiveGroup(int groupId)
        {
            Group groupToDelete = warehouseContext.Groups.FirstOrDefault(g => g.Id == groupId);
            if (groupToDelete != null)
            {
                groupToDelete.IsActive = true;
                warehouseContext.SaveChanges();


            }
        }

    }
}
