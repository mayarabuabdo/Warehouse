namespace Warehouse.Data
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<UserGroup> userGroups { get; set; }
        public bool IsActive { get; set; }
        public bool IsSystemDefine { get; set; }
    }
}
