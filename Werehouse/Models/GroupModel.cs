using System.ComponentModel.DataAnnotations;

namespace Warehouse.Models
{
    public class GroupModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsSystemDefined { get; set; }
    }
}
