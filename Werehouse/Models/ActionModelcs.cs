using AutoMapper;
using Warehouse.Data;

namespace Warehouse.Models
{
    [AutoMap(typeof(Warehouse.Data.Action), ReverseMap = true)]
    public class ActionModelcs
    {

      
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string ButtonStyle { get; set; }
    }
}
