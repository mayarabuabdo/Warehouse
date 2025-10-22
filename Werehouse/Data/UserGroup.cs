using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.Data
{
    public class UserGroup
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public AppUser User { get; set; }
        [ForeignKey("Group")]
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
